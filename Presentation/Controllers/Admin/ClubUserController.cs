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

namespace Presentation.Controllers.Admin
{

    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    [Route("api/admin/clubuser")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubUserController:ControllerBase
    
    {
        private readonly IServiceManager _manager;

        public ClubUserController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllClubUsers([FromRoute]int id, [FromQuery] ClubUserParameters clubUserParameters)
        {
           var result = await _manager.ClubUserService.GetAllUsersByClubIdAsync(id, clubUserParameters,false);
            Response.Headers.Add("X-Pagination",
              JsonSerializer.Serialize(result.metaData));
            return Ok(result.club_Users);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] ClubUserParameters clubUserParameters)
        {
            var result = await _manager.ClubUserService.GetAllUsersAsync(clubUserParameters, false);
            Response.Headers.Add("X-Pagination",
              JsonSerializer.Serialize(result.metaData));
            return Ok(result.club_Users);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ChangeApproved([FromRoute]int id)
        {
            await _manager.ClubUserService.ChangeApprovedAsync(id,true);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClubUser([FromRoute]int id)
        {
            await _manager.ClubUserService.DeleteClubUserAsync(id,true);
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> ChangeClubRole([FromBody]AdminClubUserDtoChangeRole adminClubUserDtoChangeRole )
        {
            await _manager.ClubUserService.ChangeClubRoleAsync(adminClubUserDtoChangeRole, true);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CreateClubUser([FromBody]AdminClubUserDtoInsertion adminClubUserDtoInsertion)
        {
            await _manager.ClubUserService.CreateClubUserAsync(adminClubUserDtoInsertion, true);
            return Ok();
        }

    }
}
