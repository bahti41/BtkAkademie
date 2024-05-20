using Entities.DTOs;
using Entities.Exceptions;
using Entities.RequestFeatures;
using Entities.Concrete;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controller
{
    //[ApiVersion("1.0")]// Route ile versiyonlama
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    //[Route("api/{v:apiversion}/books")] // URL ile versionlama
    [Route("api/books")] // Header İle Versiyonlama
    //[ResponseCache(CacheProfileName ="5min")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize]
        [HttpHead]
        [HttpGet(Name = "GetAllBooksAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //[ResponseCache(Duration =60)]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
        {
            var linkParameters = new LinkParameters()
            {
                BookParameters = bookParameters,
                HttpContext = HttpContext
            };

            var result = await _manager.BookService.GetAllBooksAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager
                .BookService
                .GetOneBookIdAsync(id, false);

            return Ok(book);
        }


        [Authorize(Roles = "Person, Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneBookAsync")]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookForInsertionDTO bookDto)
        {
            var book = await _manager.BookService.CreatOneBookAsync(bookDto);

            return StatusCode(201, book);//204
        }


        [Authorize(Roles = "Person, Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookForUpdateDTO bookDto)
        {
            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

            return NoContent();//204
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedBookAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);

            return NoContent();// 204
        }


        [Authorize(Roles = "Person, Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookForUpdateDTO> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();//400

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookForUpdateDto, ModelState);

            TryValidateModel(result.bookForUpdateDto);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);//422

            await _manager.BookService.SaveChangesForPatchAsync(result.bookForUpdateDto, result.book);

            return NoContent();//204
        }

        [Authorize]
        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
