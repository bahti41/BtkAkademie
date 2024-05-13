using Entities.Concrete;
using Entities.DTOs;
using Entities.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {

            var book = _manager
                .BookService
                .GetOneBookId(id, false);

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] BookForInsertionDTO bookDto)
        {

            if (bookDto is null)
                return BadRequest();//404

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);//422


            var book = _manager.BookService.CreatOneBook(bookDto);

            return StatusCode(201, book);//204
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookForUpdateDTO bookDto)
        {

            if (bookDto is null)
                return BadRequest();//404

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);//422

            _manager.BookService.UpdateOneBook(id, bookDto, true);

            return NoContent();//204
        }


        [HttpDelete("{id}")]
        public IActionResult DeletedBook([FromRoute(Name = "id")] int id)
        {

            _manager.BookService.DeleteOneBook(id, false);

            return NoContent();
        }


        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookForUpdateDTO> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = _manager.BookService.GetOneBookForPatch(id, false);

            bookPatch.ApplyTo(result.bookForUpdateDTO,ModelState);

            TryValidateModel(result.bookForUpdateDTO);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _manager.BookService.SaveChangesForPatch(result.bookForUpdateDTO, result.book);
            
            return NoContent();
        }
    }
}
