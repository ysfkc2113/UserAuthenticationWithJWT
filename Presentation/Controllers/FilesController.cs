using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController: ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile files)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");
            if(!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var path = Path.Combine(folder,files.FileName);

            using (var stream  = new FileStream(path, FileMode.Create))
            {

                await files.CopyToAsync(stream);
            }
            return Ok(new {
                file= files.FileName,
                path = path,
                size = files.Length
            });
        }
        [HttpGet("download")]
        public async Task<IActionResult> Download(string fileName)
        {
            var filepath= Path.Combine(Directory.GetCurrentDirectory(),"Media", fileName);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contentType))
            { contentType = "application/octet-stream"; }

            var bytes= await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contentType,Path.GetFileName(filepath));

        }
    }
}
