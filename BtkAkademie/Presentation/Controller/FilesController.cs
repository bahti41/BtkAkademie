using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/File")]
    public class FilesController : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile File)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // folder
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // dosya peth
            var path = Path.Combine(folder, File?.FileName);

            // Dosya Stream etme 
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }

            // response body
            return Ok(new
            {
                file = File.FileName,
                path = path,
                size = File.Length
            });
        }


        [HttpGet("Download")]
        public async Task<IActionResult> Dowloand(string fileName)
        {
            // filePath
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"Media", fileName);
            // ContentType
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            // reade
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(fileName));
        }
    }
}
