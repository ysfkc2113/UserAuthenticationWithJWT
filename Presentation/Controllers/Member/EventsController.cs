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

namespace Presentation.Controllers.Member
{

    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    [Authorize]
    [Route("api/user/events")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class EventsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public EventsController(IServiceManager manager)
        {
            _manager = manager;
        }


        //[HttpGet]
        //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //public async Task<IActionResult> GetAllEvents([FromQuery] EventParameters eventParameters)
        //{
        //    eventParameters.IsApproved= true;//kullanıcılar sadece onaylanmış etkinlikleri görebilir.
        //    var linkParameters = new LinkParameters()
        //    {
        //        EventParameters = eventParameters,
        //        HttpContext = HttpContext
        //    };

        //    var result = await _manager
        //        .EventServiceUsers
        //        .GetAllEventsAsync(linkParameters, false);

        //    Response.Headers.Add("X-Pagination",
        //        JsonSerializer.Serialize(result.metaData));

        //    return result.linkResponse.HasLinks ?
        //        Ok(result.linkResponse.LinkedEntities) :
        //        Ok(result.linkResponse.ShapedEntities);

        //}
        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Events([FromQuery] EventParameters eventParameters)
        {

            var result = await _manager
                .EventServiceUsers
                .EventsAsync(eventParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.eventDto);

        }



        [HttpGet("detail/{id:int}")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEventById([FromRoute] int id)
        {
            var result = await _manager
                .EventServiceUsers
                .GetOneEventByIdAsync(id, false);
            if (result.IsApproved == false)
                return NotFound("Etkinlik bulunamadı.");
            return Ok(result);
        }
        //bir kulübe ait bütün eventlar.
        [HttpGet("{clubId:int}")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> GetEventsByOneClub([FromRoute] int id, [FromQuery] EventParameters eventParameters)
        {

            var result = await _manager
                .EventServiceUsers
                .GetEventsByOneClub(id, eventParameters ,false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.eventDto);

        }

    }
}
