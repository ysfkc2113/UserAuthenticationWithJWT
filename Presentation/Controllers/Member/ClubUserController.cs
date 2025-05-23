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

namespace Presentation.Controllers.Member
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    // [Authorize(Roles = "User")]
    [Route("api/user/clubuser")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubUserController:ControllerBase
    {
        private readonly IServiceManager _manager;

        public ClubUserController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet("MyClubs")]
        public async Task<IActionResult> GetMyClubs([FromQuery] ClubUserParameters clubUserParameters)
        {
            var result = await _manager.ClubUserServiceUsers.GetAllUsersByClubIdForUsersAsync(clubUserParameters, HttpContext, false);
            Response.Headers.Add("X-Pagination",
              JsonSerializer.Serialize(result.metaData));
            return Ok(result.club_Users);
        }
        [HttpPost("{id:int}")]
        public async Task<IActionResult> CreateClubUser([FromRoute]int id)
        {
            await _manager.ClubUserServiceUsers.CreateClubUserForUsersAsync(id, HttpContext, true);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClubUser([FromRoute] int id)
        {
            await _manager.ClubUserServiceUsers.DeleteClubUserForUsersAsync(id, HttpContext, true);
            return NoContent();
        }
    }
}
