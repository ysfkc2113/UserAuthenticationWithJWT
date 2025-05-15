using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Security.Claims;
using System.Text.Json;


namespace Presentation.Controllers.Academician
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Authorize(Roles = "Academician")]
    [Route("api/academician/events")]
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
        public async Task<IActionResult> GetAllEvents([FromQuery] AcademicianEventParameters academicianEventParameters)
        {
            var linkParameters = new LinkParameters()
            {
                AcademicianEventParameters = academicianEventParameters,
                HttpContext = HttpContext
            };
            var result = await _manager
              .EventServiceAcademician
              .GetAllEventsAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }


        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEventById([FromRoute] int id)
        {
            var result = await _manager
                .EventServiceAcademician
                .GetOneEventByIdAcedemicianAsync(HttpContext,id, false);
            return Ok(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> CreateEvent([FromBody] AcademicianEventDtoForInsertion academicianEventDtoForInsertion)
        {
            
            var clubEvent = await _manager.EventServiceAcademician.CreateOneEventForAcademicianAsync(academicianEventDtoForInsertion, HttpContext,true);
            return StatusCode(201, clubEvent); // CreatedAtRoute()
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent([FromRoute(Name = "id")] int id)
        {

            await _manager.EventServiceAcademician.DeleteOneEventForAcademicianAsync(id,HttpContext, true);
            return Ok();
        }

        //Update işleminden sonra girilmezse Event Date bozuluyor
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] AcademicianEventDtoForUpdate  academicianEventDtoForUpdate)
        {
            await _manager.EventServiceAcademician.UpdateEventForAcademicianAsync(id, academicianEventDtoForUpdate, HttpContext, true);
            return Ok();
        }

        //bu patch sadece approved için.
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchEvent([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<EventDtoForPatchApproved> eventPatch)
        {
            if (eventPatch is null)
                return BadRequest(); // 400

            var result = await _manager.EventServiceAcademician
                .GetOneEventDtoForPatchApprovedForAcademician(HttpContext, id, false);

            eventPatch.ApplyTo(result.eventDtoForUpdate, ModelState);

            TryValidateModel(result.eventDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _manager.EventServiceAcademician.SaveChangesForPatchApprovedForAcademicianAsync(result.eventDtoForUpdate, result.clubEvent, HttpContext, true);

            return NoContent(); // 204
        }
    }
}
