using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
using System.Xml.Linq;

namespace Presentation.Controllers.Admin
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/admin/events")]
    [ApiExplorerSettings(GroupName = "v1")]
    //[ResponseCache(CacheProfileName="5min")]
    public class EventsController : ControllerBase

    {
        private readonly IServiceManager _manager;

        public EventsController(IServiceManager serviceManager)
        {
            _manager = serviceManager;
        }


        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllEvents([FromQuery] EventParameters eventParameters)
        {
            var linkParameters = new LinkParameters()
            {
                EventParameters = eventParameters,
                HttpContext = HttpContext
            };

            var result = await _manager
                .EventService
                .GetAllEventsAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);

        }


        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEventById([FromRoute]int id)
        {
            var result = await _manager
                .EventService
                .GetOneEventByIdAsync(id,false);
            return Ok(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> CreateEvent([FromBody] EventDtoForInsertion eventDtoForInsertion)
        {         
            var contextUserName = await _manager.UsersService.GetUserNameByHttpContextAsync(HttpContext);
            var clubEvent = await _manager.EventService.CreateOneEventAsync(eventDtoForInsertion, contextUserName);
            return StatusCode(201, clubEvent); // CreatedAtRoute()
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent([FromRoute(Name = "id")] int id)
        {
            await _manager.EventService.DeleteOneEventAsync(id, true);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] AdminEventDtoForUpdate dtoForUpdateAdmin)
        {
            await _manager.EventService.UpdateEventForAdminAsync(id, dtoForUpdateAdmin, true);
            return Ok();
        }

        //bu patch sadece approved için.
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchEvent([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<EventDtoForPatchApproved> eventPatch)
        {
            
            var contextUserName = await _manager.UsersService.GetUserNameByHttpContextAsync(HttpContext);
            if (eventPatch is null)
                return BadRequest(); // 400

            var result = await _manager.EventService.GetOneEventDtoForPatchApproved(id, false);

            eventPatch.ApplyTo(result.eventDtoForUpdate, ModelState);

            TryValidateModel(result.eventDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _manager.EventService.SaveChangesForPatchApprovedAsync(result.eventDtoForUpdate, result.clubEvent, contextUserName, true);

            return NoContent(); // 204
        }


        //[HttpCacheExpiration(CacheLocation =CacheLocation.Private,MaxAge = 40)]
        //[ResponseCache(Duration = 60)]
        [Authorize]
       // [HttpHead]//respons body siz sadece header ile oluşturulur
        [HttpOptions]
        public IActionResult OptionsEvents()
        {
            Response.Headers.Add("Allow", "Get,Put,Post,Patch,Delete,Options");
            return Ok();
        }
    }
}
