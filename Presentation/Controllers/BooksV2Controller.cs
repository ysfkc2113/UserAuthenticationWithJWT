using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    //[ApiVersion("2.0",Deprecated = true)]// yayından kaldırdık //bu kısmı extensionda da yaptık.
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/books")]
    public class BooksV2Controller : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksV2Controller(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _serviceManager.BookService.GetAllBooksAsync(false);
            var booksV2 = books.Select(m => new
            {
                Title = m.Title,
                Id = m.Id
            });
            

            return Ok(booksV2);
        }
    }
}
