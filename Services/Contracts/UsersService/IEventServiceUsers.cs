using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.UsersService
{
    public interface IEventServiceUsers
    { 
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllEventsAsync(LinkParameters linkParameters,
            bool trackChanges);
        Task<EventDto> GetOneEventByIdAsync(int id, bool trackChanges);
        Task<(IEnumerable<EventDto> eventDto, MetaData metaData)> EventsAsync(EventParameters eventParameters,bool trackChanges);
        Task<(IEnumerable<EventDto> eventDto, MetaData metaData)> GetEventsByOneClub(int id, EventParameters eventParameters, bool trackChanges);
    }
}
