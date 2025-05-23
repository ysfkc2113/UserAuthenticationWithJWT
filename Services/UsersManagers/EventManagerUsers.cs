using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.UsersManagers
{
    public class EventManagerUsers: IEventServiceUsers
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IEventLinks _eventLinks;

        public EventManagerUsers(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper,
            IEventLinks eventLinks)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _eventLinks = eventLinks;
        }
        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllEventsAsync(LinkParameters linkParameters,bool trackChanges)
        {
            var eventsWithMetaData = await _manager
                .Event
                .GetAllEventsAsync(linkParameters.EventParameters, trackChanges);

            var eventDto = _mapper.Map<IEnumerable<EventDto>>(eventsWithMetaData);
            var links = _eventLinks.TryGenerateLinks(eventDto,
                linkParameters.EventParameters.Fields,
                linkParameters.HttpContext);

            return (linkResponse: links, metaData: eventsWithMetaData.MetaData);
        }
        public async Task<EventDto> GetOneEventByIdAsync(int id, bool trackChanges)
        {
            var clubEvent = await GetOneEventByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<EventDto>(clubEvent);
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
