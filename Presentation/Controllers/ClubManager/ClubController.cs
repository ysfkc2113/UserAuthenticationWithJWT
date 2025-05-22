using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.ClubManager
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    // [Authorize(Roles = "Club Manager")]
    [Route("api/clubmanager/clubs")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ClubController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public async Task<IActionResult> GetOneClubById()
        {
            var club = await _manager.ClubServiceClubLeader.GetOneClubByIdForClubManagerAsync(HttpContext, true);
            return Ok(club);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClub([FromBody] ClubManagerDtoForUpdate clubManagerDtoForUpdate)
        {

            var club = await _manager.ClubServiceClubLeader.UpdateClubForClubManagerAsync(clubManagerDtoForUpdate, HttpContext, true);
            return Ok(club);
        }
    }
}
