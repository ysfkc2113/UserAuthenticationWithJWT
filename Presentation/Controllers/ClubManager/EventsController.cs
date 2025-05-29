using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers.ClubManager
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    [Authorize(Roles = "Club Manager")]
    [Route("api/clubmanager/events")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class EventsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public EventsController(IServiceManager manager)
        {
            _manager = manager;
        }


        
        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllEventsMyClub([FromQuery] ClubManagerEventParameters clubManagerEventParameters)
        {
            var result = await _manager
               .EventServiceClubLeader
               .GetAllEventsForClubManagerAsync(clubManagerEventParameters, HttpContext, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.eventDto);
        }

        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEventById([FromRoute] int id)
        {
            var result = await _manager
                .EventService
                .GetOneEventByIdAsync(id, false);
            if (result.IsApproved == false)
                return NotFound("Etkinlik bulunamadı.");
            return Ok(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> CreateEvent([FromBody] AcademicianEventDtoForInsertion academicianEventDtoForInsertion)
        {

            var clubEvent = await _manager.EventServiceClubLeader.CreateOneEventForClubLeaderAsync(academicianEventDtoForInsertion, HttpContext, true);
            return StatusCode(201, clubEvent); // CreatedAtRoute()
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent([FromRoute(Name = "id")] int id)
        {

            await _manager.EventServiceClubLeader.DeleteOneEventForClubManagerAsync(id, HttpContext, true);
            return Ok();
        }

        //Update işleminden sonra girilmezse Event Date bozuluyor
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] AcademicianEventDtoForUpdate academicianEventDtoForUpdate)
        {
            await _manager.EventServiceClubLeader.UpdateEventForClubManagerAsync(id, academicianEventDtoForUpdate, HttpContext, true);
            return Ok();
        }
    }
}
