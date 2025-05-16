using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.AcademcianService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.AcademicianManagers
{
    public class EventManagerAcademician : IEventServiceAcademician
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IEventLinks _eventLinks;

        public EventManagerAcademician(IRepositoryManager repositoryManager, IMapper mapper, IEventLinks eventLinks)
        {
            _manager = repositoryManager;
            _mapper = mapper;
            _eventLinks = eventLinks;
        }

        public async Task<EventDto> CreateOneEventForAcademicianAsync
            (AcademicianEventDtoForInsertion academicianEventDtoForInsertion, HttpContext httpContext,bool trackChanges)
        {
         var userName=await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByAcademicianName(userName, trackChanges);
            if (club == null) { 
                throw new Exception("Her hangi bir külübe danışman değilsiniz."); 
            }
            var entity = _mapper.Map<Event>(academicianEventDtoForInsertion);
            _manager.Event.CreateOneEventForAcademician(entity,userName,club.ClubId);
            await _manager.SaveAsync();
            return _mapper.Map<EventDto>(entity);
        }

        public async Task DeleteOneEventForAcademicianAsync(int id, HttpContext httpContext, bool trackChanges)
        {
            var userName= await GetUserNameByHttpContextAsync(httpContext);
            var club=await _manager.Club.GetOneClubByAcademicianName (userName, trackChanges);
            if (club == null)
            {
                throw new Exception("Her hangi bir külübe danışman değilsiniz.");
            }
            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);
            if (clubEvent.ClubId != club.ClubId)
            {
                throw new Exception("Bu etkinlik sizin yetki alanınızda değil.");
            }
            _manager.Event.Delete(clubEvent);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllEventsAsync(LinkParameters linkParameters, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(linkParameters.HttpContext);
            //EventParameters eventParameters = _mapper.Map<EventParameters>(linkParameters.AcademicianEventParameters);
           
            var club = await _manager.Club.GetOneClubByAcademicianName(userName,trackChanges);
            if (club == null) { throw new Exception("Her hangi bir külübe danışman değilsiniz."); }

            //eventParameters.ClubId = club.ClubId;
            var eventsWithMetaData = await _manager
                .Event
                .GetAllEventsForAcademicianAsync(linkParameters.AcademicianEventParameters, club.ClubId, trackChanges);

            var eventDto = _mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);
            var links = _eventLinks.TryGenerateLinks(eventDto,
                linkParameters.AcademicianEventParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse: links, metaData: eventsWithMetaData.MetaData);

        }

        public async Task<EventDto> GetOneEventByIdAcedemicianAsync(HttpContext httpContext,int id, bool trackChanges)
        {
            var userName= await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByAcademicianName(userName, trackChanges);
            if (club == null) { throw new Exception("Her hangi bir külübe danışman değilsiniz."); }

            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);
            if (clubEvent.ClubId != club.ClubId) 
            {
                throw new Exception("Bu etkinlik sizin yetki alanınızda değil.");
            }
            return _mapper.Map<EventDto>(clubEvent);
        }

        public async Task<(EventDtoForPatchApproved eventDtoForUpdate, Event clubEvent)> GetOneEventDtoForPatchApprovedForAcademician(HttpContext httpContext, int id, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByAcademicianName(userName, trackChanges);
            if (club == null) { throw new Exception("Her hangi bir külübe danışman değilsiniz."); }
            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);
            if (clubEvent.ClubId != club.ClubId)
            {
                throw new Exception("Bu etkinlik sizin yetki alanınızda değil.");
            }
            var eventDtoForUpdate = _mapper.Map<EventDtoForPatchApproved>(clubEvent);
            return (eventDtoForUpdate, clubEvent);
        }

        public async Task SaveChangesForPatchApprovedForAcademicianAsync( EventDtoForPatchApproved  eventDtoForPatchApproved, Event clubEvent, HttpContext httpContext, bool trackChanges)
        {
            var userName= await GetUserNameByHttpContextAsync(httpContext);
            clubEvent.ApprovedByUserName =userName ;
            clubEvent.ApprovedTime = DateTime.Now;
            _mapper.Map(eventDtoForPatchApproved, clubEvent);
            _manager.Event.Update(clubEvent);
            await _manager.SaveAsync();
        }

       
        public async Task UpdateEventForAcademicianAsync
            (int id, AcademicianEventDtoForUpdate academicianEventDtoForUpdate, HttpContext httpContext, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByAcademicianName(userName, trackChanges);
            if (club == null) { throw new Exception("Her hangi bir külübe danışman değilsiniz."); }
            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);
            if (clubEvent.ClubId != club.ClubId)
            {
                throw new Exception("Bu etkinlik sizin yetki alanınızda değil.");
            }
            _mapper.Map(academicianEventDtoForUpdate, clubEvent);
            _manager.Event.UpdateOneEvent(clubEvent);
            await _manager.SaveAsync();
        }
        private async Task<Event> GetOneEventByIdAndCheckExists(int id, bool trackChanges)
        {
            // check entity 
            var entity = await _manager.Event.GetOneEventByIdAsync(id, trackChanges);

            if (entity is null)
                throw new EventNotFoundException(id);

            return entity;
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
    }
}
