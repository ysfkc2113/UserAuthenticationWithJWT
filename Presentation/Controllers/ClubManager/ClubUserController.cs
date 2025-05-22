using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers.ClubManager
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    // [Authorize(Roles = "Club Manager")]
    [Route("api/clubmanager/clubuser")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubUserController:ControllerBase
    {

        private readonly IServiceManager _manager;

        public ClubUserController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClubUsers([FromQuery] ClubUserParameters clubUserParameters)
        {
            var result = await _manager.ClubUserServiceClubLeader.GetAllUsersByClubIdForClubManagerAsync(clubUserParameters, HttpContext, false);
            Response.Headers.Add("X-Pagination",
              JsonSerializer.Serialize(result.metaData));
            return Ok(result.club_Users);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ChangeApproved([FromRoute] int id)
        {
            await _manager.ClubUserServiceClubLeader.ChangeApprovedForClubManagerAsync(id, HttpContext, true);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ChangeClubRole([FromBody] AdminClubUserDtoChangeRole adminClubUserDtoChangeRole)
        {
            await _manager.ClubUserServiceClubLeader.ChangeClubRoleForClubManagerAsync(adminClubUserDtoChangeRole, HttpContext, true);
            return Ok();
        }
    }
}
