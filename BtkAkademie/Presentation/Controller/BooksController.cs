﻿using Entities.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {

            var book = await _manager
                .BookService
                .GetOneBookIdAsync(id, false);

            return Ok(book);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookForInsertionDTO bookDto)
        {

            var book = await _manager.BookService.CreatOneBookAsync(bookDto);

            return StatusCode(201, book);//204
        }

        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookForUpdateDTO bookDto)
        {
            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

            return NoContent();//204
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedBookAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);

            return NoContent();// 204
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookForUpdateDTO> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();//400

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookForUpdateDto,ModelState);

            TryValidateModel(result.bookForUpdateDto);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);//422

            await _manager.BookService.SaveChangesForPatchAsync(result.bookForUpdateDto, result.book);
            
            return NoContent();//204
        }
    }
}