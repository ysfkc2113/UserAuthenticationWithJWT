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
    [ApiController]
    [Route("api/category")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task <IActionResult>  GetAllCategories()
        {
            return Ok(await _manager.CategoryService.GetAllCategoriesAsync(false));

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneCategoryById([FromRoute] int id)
        {
            var category = await _manager.CategoryService.GteOneCategoryByIdAsync(id, false);
            
            return Ok(category);
        }
    }
}
