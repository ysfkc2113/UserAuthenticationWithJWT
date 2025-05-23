using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Controllers.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name ="GetRoot")]
        public async Task<IActionResult> GetRoot([FromHeader(Name ="Accept")] string mediaType) { 
        
            if(mediaType.Contains("application/vnd.medeniyet.apiroot"))
            {
                var list = new List<Link>
                {
                    new Link
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext,nameof (GetRoot),new{}),
                        Rel="_self",
                        Method = "GET",

                    },
                    new Link
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext,nameof (EventsController.GetAllEvents),new{}),
                        Rel="events",
                        Method = "GET",

                    },
                    new Link
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext,nameof (EventsController.GetEventById),new{}),
                        Rel="events",
                        Method = "Post",

                    }

                };
                return Ok(list);
            }
            return NoContent();
        }
    }
}
