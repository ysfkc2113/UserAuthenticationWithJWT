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
    [Route("api/user/clubs")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubController:ControllerBase
    {
        private readonly IServiceManager _manager;

        public ClubController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllClubs([FromQuery] ClubParameters clubParameters)
        {
            var result = await _manager
                .ClubServiceUsers
                .GetAllClubsAsync(clubParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.clubDto);

        }


    }
}
