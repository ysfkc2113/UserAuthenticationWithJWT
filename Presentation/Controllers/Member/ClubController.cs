using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
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
    [Route("api/user/clubs")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubController:ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;

        public ClubController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }


        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllClubs([FromQuery] ClubParameters clubParameters)
        {
            var result = await _manager
                .ClubServiceUsers
                .GetAllClubsAsync(clubParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.clubDto);

        }
        //kulüp detay sayfası için kullanılıyor
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneClubById([FromRoute] int id)
        {
            var club = await _manager.ClubService.GetOneClubByIdAsync(id,false);
            EventParameters eventParameters = new EventParameters() { PageNumber = 1,PageSize=3 };
            var result = await _manager
            .EventServiceUsers
                       .GetEventsByOneClub(id, eventParameters, false);
            if (club == null)
                throw new Exception("Böyle bir kulüp bulunamadı.");
            var memberscount= await _manager.ClubUserService.GetAllUsersByClubIdAsync(id);
            
            var clubdetaildto = new ClubDetailDtoUsers() {
                Club=club,Events= result.eventDto.ToList() ,
                MemberCount=memberscount,
                EventCount= result .metaData.TotalCount};
            return Ok(clubdetaildto);
        }
        


    }
}
