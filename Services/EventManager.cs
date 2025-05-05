using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class EventManager : IEventService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IEventLinks _eventLinks;
        private readonly IClubService _clubService;

        public EventManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper,
            IEventLinks eventLinks,
            IClubService clubService)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _eventLinks = eventLinks;
            _clubService = clubService;
        }

        public async Task ApproveEventAsync(int id, string approvedByUserId, bool trackChanges)
        {
            var event1 = await GetOneEventByIdAndCheckExists(id, trackChanges);
            _manager.Event.ChangeApprovedEvent(event1, approvedByUserId);
            await _manager.SaveAsync();

        }

        public async Task<EventDto> CreateOneEventAsync(EventDtoForInsertion eventDto,string userId)
        {
            var club = await _clubService.GetOneClubByIdAsync(eventDto.ClubId, false);

            var entity = _mapper.Map<Event>(eventDto);
            entity.PublishedById = userId;
            _manager.Event.CreateOneEvent(entity);
            await _manager.SaveAsync();
            return _mapper.Map<EventDto>(entity);
        }

        public async Task DeleteOneEventAsync(int id, bool trackChanges)
        {
            var entity = await GetOneEventByIdAndCheckExists(id, trackChanges);
            _manager.Event.DeleteOneEvent(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)>
            GetAllEventsAsync(LinkParameters linkParameters,
            bool trackChanges)
        {
            //silinecek   if(!linkParameters.EventParameters.ValidPriceRange)
            //     throw new PriceOutofRangeBadRequestException();

            var eventsWithMetaData = await _manager
                .Event
                .GetAllEventsAsync(linkParameters.EventParameters, trackChanges);

            var eventDto = _mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);
            var links = _eventLinks.TryGenerateLinks(eventDto,
                linkParameters.EventParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse: links, metaData: eventsWithMetaData.MetaData);
        }

        //public async Task<List<Event>> GetAllEventsAsync(bool trackChanges)
        //{
        //    var events = await _manager.Event.GetAllEventsAsync(trackChanges);
        //    return events;
        //}

        public async Task<List<Event>> GetAllEventsWithDetailsAsync(bool trackChanges)
        {
            return await _manager.Event.GetAllEventsWithDetailsAsync(trackChanges);
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetApprovedEventsAsync(LinkParameters linkParameters, bool trackChanges)
        {


            var eventsWithMetaData = await _manager
                .Event
                .GetApprovedEventsAsync(linkParameters.EventParameters, trackChanges);

            var eventDto = _mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);
            var links = _eventLinks.TryGenerateLinks(eventDto,
                linkParameters.EventParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse: links, metaData: eventsWithMetaData.MetaData);

        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetEventsByClubIdAsync(int clubId, LinkParameters linkParameters, bool trackChanges)
        {
          var events= await _manager.Event.GetEventsByClubIdAsync(clubId, linkParameters.EventParameters,trackChanges);
            var eventDto = _mapper.Map<IEnumerable<EventDto>>(events);
            var links = _eventLinks.TryGenerateLinks(eventDto,
                linkParameters.EventParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse: links, metaData: events.MetaData);

        }

        public async Task<EventDto> GetOneEventByIdAsync(int id, bool trackChanges)
        {
            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);

            if (clubEvent is null)
                throw new EventNotFoundException(id);
            return _mapper.Map<EventDto>(clubEvent);
        }

        public async Task<(EventDtoForUpdate eventDtoForUpdate, Event clubEvent)>
            GetOneEventForPatchAsync(int id, bool trackChanges)
        {
            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);
            var eventDtoForUpdate = _mapper.Map<EventDtoForUpdate>(clubEvent);
            return (eventDtoForUpdate, clubEvent);
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetPendingApprovalEventsAsync(LinkParameters linkParameters, bool trackChanges)
        {
            var eventsWithMetaData = await _manager
                .Event
                .GetPendingApprovalEventsAsync(linkParameters.EventParameters, trackChanges);

            var eventDto = _mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);
            var links = _eventLinks.TryGenerateLinks(eventDto,
                linkParameters.EventParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse: links, metaData: eventsWithMetaData.MetaData);

        }

        public async Task SaveChangesForPatchAsync(EventDtoForUpdate eventDtoForUpdate, Event clubEvent , bool trackChanges)
        {
            _mapper.Map(eventDtoForUpdate, clubEvent);
            _manager.Event.Update(clubEvent);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneEventAsync(int id,
            EventDtoForUpdate eventDto,
            bool trackChanges)
        {
            var entity = await GetOneEventByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map(eventDto,entity);
            _manager.Event.Update(entity);
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
    }
}
