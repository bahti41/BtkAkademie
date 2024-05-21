using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/Categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _services;

        public CategoriesController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _services.CategoryService.GetAllCategoriesAsync(false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneCategories([FromRoute] int id)
        {
            return Ok(await _services.CategoryService.GetOneCategoryByIdAsync(id,false));
        }
    }
}
