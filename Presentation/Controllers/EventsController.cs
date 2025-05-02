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
    [ApiExplorerSettings(GroupName ="v1")]
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
        [ResponseCache(Duration = 60)]
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneEventAsync([FromRoute(Name = "id")] int id)
        {
            var clubEvent = await _manager
            .EventService
            .GetOneEventByIdAsync(id, false);

            return Ok(clubEvent);
        }
        [Authorize(Roles ="Admin, Academician")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneEventAsync")]
        public async Task<IActionResult> CreateOneEventAsync([FromBody] EventDtoForInsertion eventDto)
        {
            var clubEvent = await _manager.EventService.CreateOneEventAsync(eventDto);
            return StatusCode(201, clubEvent); // CreatedAtRoute()
        }

        [Authorize(Roles = "Admin, Academician,Club Manager")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneEventAsync([FromRoute(Name = "id")] int id,
            [FromBody] EventDtoForUpdate eventDto)
        {
            await _manager.EventService.UpdateOneEventAsync(id, eventDto, false);
            return NoContent(); // 204
        }

        [Authorize(Roles = "Admin, Academician")]
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

        [Authorize]
        [HttpGet("details")]
        public async Task <IActionResult> GetAllEventsWithDetailsAsync()
        {
            return Ok(await _manager.EventService.GetAllEventsWithDetailsAsync(false));
        }
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
            var events= await _manager.EventService.GetPendingApprovalEventsAsync(linkParameters, false);
            return Ok(events);
        
        }



        

    }
}
