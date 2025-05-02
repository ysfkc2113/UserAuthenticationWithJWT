using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
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

namespace Presentation.Controllers
{

    //[ApiVersion("1.0")] //bu kısmı extensionda da yaptık.
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/events")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    //[ResponseCache(CacheProfileName="5min")]
    public class EventsController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public EventsController(IServiceManager manager)
        {
            _manager = manager;
        }
        [Authorize]
        [HttpHead]//respons body siz sadece header ile oluşturulur
        [HttpGet(Name = "GetAllEventsAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //[HttpCacheExpiration(CacheLocation =CacheLocation.Private,MaxAge = 40)]
        //[ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAllEventsAsync([FromQuery] EventParameters eventParameters)
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
        [Authorize]
        [HttpGet("byid/{id:int}")]
        public async Task<IActionResult> GetOneEventAsync([FromRoute(Name = "id")] int id)
        {
            var clubEvent = await _manager
            .EventService
            .GetOneEventByIdAsync(id, false);

            return Ok(clubEvent);
        }
        [Authorize(Roles = "Admin, Academician")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneEventAsync")]
        public async Task<IActionResult> CreateOneEventAsync([FromBody] EventDtoForInsertion eventDto)
        {// 1. Get the HttpContext
            var httpContext = HttpContext;

            // 2. Get the User from HttpContext
            var user = httpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not authenticated.");
            }

            // 3. Get the User's ID from the JWT token's claims
            var userId = user.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID claim is missing in the JWT token.");
            }
            var clubEvent = await _manager.EventService.CreateOneEventAsync(eventDto, userId);
            return StatusCode(201, clubEvent); // CreatedAtRoute()
        }

        [Authorize(Roles = "Admin, Academician,Club Manager")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateOneEventAsync([FromRoute(Name = "id")] int id,
            [FromBody] EventDtoForUpdate eventDto)
        {
            await _manager.EventService.UpdateOneEventAsync(id, eventDto, false);
            return NoContent(); // 204
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneEventAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.EventService.DeleteOneEventAsync(id, false);
            return NoContent();
        }

        [Authorize(Roles = "Admin, Academician,Club Manager")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneEventAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<EventDtoForUpdate> eventPatch)
        {

            if (eventPatch is null)
                return BadRequest(); // 400

            var result = await _manager.EventService.GetOneEventForPatchAsync(id, false);

            eventPatch.ApplyTo(result.eventDtoForUpdate, ModelState);

            TryValidateModel(result.eventDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.EventService.SaveChangesForPatchAsync(result.eventDtoForUpdate, result.clubEvent);

            return NoContent(); // 204
        }

        [Authorize]
        [HttpOptions]
        public IActionResult OptionsEvents()
        {
            Response.Headers.Add("Allow", "Get,Put,Post,Patch,Delete,Options");
            return Ok();
        }
        //çalıştı
        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> GetAllEventsWithDetailsAsync()
        {
            return Ok(await _manager.EventService.GetAllEventsWithDetailsAsync(false));
        }
        //çalıştı
        [Authorize]
        [HttpGet("beklenenler")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetPendingApprovalEventsAsync([FromQuery] EventParameters eventParameters)
        {
            var linkParameters = new LinkParameters()
            {
                EventParameters = eventParameters,
                HttpContext = HttpContext
            };
            var events = await _manager.EventService.GetPendingApprovalEventsAsync(linkParameters, false);
            return Ok(events);
        }
        //çalıştı ama boş
        [Authorize]
        [HttpGet("onaylananlar")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetApprovedEventsAsync([FromQuery] EventParameters eventParameters)
        {
            var linkParameters = new LinkParameters()
            {
                EventParameters = eventParameters,
                HttpContext = HttpContext
            };
            var events = await _manager.EventService.GetApprovedEventsAsync(linkParameters, false);
            return Ok(events);

        }
        //çalıştı
        [Authorize]
        [HttpGet("byclub/{clubid:int}")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEventsByClubIdAsync([FromRoute] int clubid, [FromQuery] EventParameters eventParameters)
        {
            var linkParameters = new LinkParameters()
            {
                EventParameters = eventParameters,
                HttpContext = HttpContext
            };
            var clubEvents = await _manager.EventService.GetEventsByClubIdAsync(clubid, linkParameters, false);
            return Ok(clubEvents);
        }

        [Authorize("Admin")]
        [HttpPut("approved/{id:int}")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> approveeventasync([FromRoute] int id)
        {
            // 1. get the httpcontext
            var httpContext = HttpContext;

           

            if (httpContext == null)
            {
                return StatusCode(500, "httpcontext is not available.");
            }

            // 2. Get the User from HttpContext
            var user = httpContext.User;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not authenticated.");
            }

            // 3. Get the User's ID from the JWT token's claims
            var userId = user.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID claim is missing in the JWT token.");
            }

            await _manager.EventService.ApproveEventAsync(id, userId, true);
            return Ok();
        }




    }
}
