using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.ClubLeaderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.ClubLeaderManagers
{
    public class EventManagerClubLeader: IEventServiceClubLeader
    {

        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IEventLinks _eventLinks;

        public EventManagerClubLeader(IRepositoryManager manager, IMapper mapper, IEventLinks eventLinks)
        {
            _manager = manager;
            _mapper = mapper;
            _eventLinks = eventLinks;
        }

        public async Task<(IEnumerable<EventDto> eventDto, MetaData metaData)> 
            GetAllEventsForClubManagerAsync(ClubManagerEventParameters clubManagerEventParameters, HttpContext httpContext, bool trackChanges)
        {
            var userName_club = await GetUserNameAndCLubByHttpContextAsync(httpContext);
            //EventParameters eventParameters = _mapper.Map<EventParameters>(linkParameters.AcademicianEventParameters);

           
  
            var eventsWithMetaData = await _manager
                .Event
                .GetAllEventsForClubManagerAsync(clubManagerEventParameters, userName_club.club.ClubId, trackChanges);

            var eventDto = _mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);


            return ( eventDto, metaData: eventsWithMetaData.MetaData);

        }
        public async Task<EventDto> CreateOneEventForClubLeaderAsync(AcademicianEventDtoForInsertion academicianEventDtoForInsertion, HttpContext httpContext, bool trackChanges)

        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByClubManagerName(userName, trackChanges);
            if (club == null)
            {
                throw new Exception("Her hangi bir kulübün yönetcisi değilsiniz.");
            }
            var entity = _mapper.Map<Event>(academicianEventDtoForInsertion);
            _manager.Event.CreateOneEventForClubManager(entity, userName, club.ClubId);
            await _manager.SaveAsync();
            return _mapper.Map<EventDto>(entity);
        }
        public async Task DeleteOneEventForClubManagerAsync(int id, HttpContext httpContext, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByClubManagerName(userName, trackChanges);
            if (club == null)
            {
                throw new Exception("Her hangi bir kulübün yönetcisi değilsiniz.");
            }
            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);
            if (clubEvent.ClubId != club.ClubId)
            {
                throw new Exception("Bu etkinlik sizin yetki alanınızda değil.");
            }
            _manager.Event.Delete(clubEvent);
            await _manager.SaveAsync();
        }
        public async Task UpdateEventForClubManagerAsync
            (int id, AcademicianEventDtoForUpdate academicianEventDtoForUpdate, HttpContext httpContext, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByClubManagerName(userName, trackChanges);
            if (club == null) { throw new Exception("Her hangi bir külübe yönetici değilsiniz."); }
            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);
            if (clubEvent.ClubId != club.ClubId)
            {
                throw new Exception("Bu etkinlik sizin yetki alanınızda değil.");
            }
            _mapper.Map(academicianEventDtoForUpdate, clubEvent);
            _manager.Event.UpdateOneEventForClubManager(clubEvent);
            await _manager.SaveAsync();
        }





        private async Task<string> GetUserNameByHttpContextAsync(HttpContext httpContext)
        {
            var user = httpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            //var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("User ID claim is missing in the JWT token.");
            }
            return userName;
        }
        private async Task<(string userName, Club club)> GetUserNameAndCLubByHttpContextAsync(HttpContext httpContext)
        {
            var user = httpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            //var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("User ID claim is missing in the JWT token.");
            }

            var club = await _manager.Club.GetOneClubByClubManagerName(userName, false);
            if (club == null) { throw new Exception("Her hangi bir külüpte yönetici değilsiniz."); }
            return (userName, club);
        }
        private async Task<Event> GetOneEventByIdAndCheckExists(int id, bool trackChanges)
        {
            // check entity 
            var entity = await _manager.Event.GetOneEventByIdAsync(id, trackChanges);

            if (entity is null)
                throw new EventNotFoundException(id);

            return entity;
        }
    }
}
