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
    [Route("api/club")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    public class ClubController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ClubController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task <IActionResult>  GetAllClubs()
        {
            return Ok(await _manager.ClubService.GetAllClubsAsync(false));

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneClubById([FromRoute] int id)
        {
            var club = await _manager.ClubService.GteOneClubByIdAsync(id, false);
            
            return Ok(club);
        }
    }
}
