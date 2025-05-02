using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    //[ApiVersion("2.0",Deprecated = true)]// yayından kaldırdık //bu kısmı extensionda da yaptık.
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/events")]
    public class EventsV2Controller : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EventsV2Controller(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventsAsync()
        {
           // var events = await _serviceManager.EventService.GetAllEventsAsync(false);
            var events = await _serviceManager.EventService.GetAllEventsWithDetailsAsync(false);
            
            var eventsV2 = events.Select(m => new
            {
                Title = m.Title,
                Id = m.Id
            });
            

            return Ok(eventsV2);
        }
    }
}
