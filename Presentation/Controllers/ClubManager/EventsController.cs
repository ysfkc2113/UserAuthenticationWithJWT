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
    [Authorize(Roles = "Admin,Club Manager")]
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
        public async Task<IActionResult> GetAllEvents([FromQuery] EventParameters eventParameters)
        {
            eventParameters.IsApproved = true;//kullanıcılar sadece onaylanmış etkinlikleri görebilir.
            var linkParameters = new LinkParameters()
            {
                EventParameters = eventParameters,
                HttpContext = HttpContext
            };
            
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
            // var userId = user.FindFirst(ClaimTypes.Name)?.Value;
            var userId = "6891d6cb-f4e2-4f5c-a91e-398b32b09255";
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID claim is missing in the JWT token.");
            }
             //hangi kulube üyu bul ıd
             //bu ıd ile etkinlikleri getir

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
        public async Task<IActionResult> GetEventById([FromRoute] int id)
        {
            var result = await _manager
                .EventService
                .GetOneEventByIdAsync(id, false);
            if (result.IsApproved == false)
                return NotFound("Etkinlik bulunamadı.");
            return Ok(result);
        }


    }
}
