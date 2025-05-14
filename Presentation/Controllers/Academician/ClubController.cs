using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers.Academician
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    // [Authorize(Roles = "Academician")]
    [Route("api/academician/clubs")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubController : ControllerBase
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
                .ClubService
                .GetAllClubsAsync(clubParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.clubDto);

        }
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetOneClubById([FromRoute] int id)
        {
            var club = await _manager.ClubService.GetOneClubByIdAsync(id, true);
            return Ok(club);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClub([FromBody] AdminClubDtoForUpdate adminClubDtoForUpdate, [FromRoute] int id)
        {

            var club = await _manager.ClubService.UpdateClubAsync(adminClubDtoForUpdate, id, true);
            return Ok(club);
        }


    }
}
