using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    //[ApiVersion("2.0",Deprecated =true)] //Routelar ile versionlama
    [ApiController]
    //[Route("api/{v:apiversion}/books")] //URL ile Versiyonlama
    [Route("api/books")] //Header ile versionlama
    public class BooksV2Controller:ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksV2Controller(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GEtAllBookAsync()
        {
            var books = await _serviceManager.BookService.GetAllBooksAsync(false);
            var bookV2 = books.Select(b => new
            {
                Id = b.Id,
                Title = b.Title
            });
            return Ok(bookV2);
        }
    }
}
