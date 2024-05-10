using BookDemo.Data;
using BookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookDemo.Controllers
{
    [Route("api/Books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books;
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = ApplicationContext
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
            if (book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();

                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            var entity = ApplicationContext
                .Books
                .Find(b => b.Id.Equals(id));

            if (entity is null)
                return NotFound();

            if (id != book.Id)
                return BadRequest();

            ApplicationContext.Books.Remove(entity);
            book.Id = entity.Id;
            ApplicationContext.Books.Add(book);
            return Ok(book);
        }


        [HttpDelete]
        public IActionResult DeletedAllBook()
        {
            ApplicationContext.Books.Clear();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletedAllBook([FromRoute(Name ="id")] int id)
        {
            var entity = ApplicationContext
                .Books
                .Find ( b => b.Id.Equals(id));

            if (entity is null)
                return NotFound(new
                {
                    StatusCode = 404,
                    messag = $"Book With id:{id} could not found."
                });
            ApplicationContext.Books.Remove(entity);
            return NoContent(); 

        }
    }
}
