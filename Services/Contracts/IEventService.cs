using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IEventService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllEventsAsync(LinkParameters linkParameters,
            bool trackChanges);
        Task<EventDto> GetOneEventByIdAsync(int id, bool trackChanges);
        Task<EventDto> CreateOneEventAsync(EventDtoForInsertion clubEvent,string userId);
        Task UpdateOneEventAsync(int id, EventDtoForUpdate eventDto, bool trackChanges);
        //For Admin
        Task UpdateEventForAdminAsync(int id, EventDtoForUpdateAdmin eventDtoForUpdateAdmin, bool trackChanges);
        Task DeleteOneEventAsync(int id, bool trackChanges);

        Task<(EventDtoForUpdate eventDtoForUpdate, Event clubEvent)> GetOneEventForPatchAsync(int id, bool trackChanges);

        Task SaveChangesForPatchAsync(EventDtoForUpdate eventDtoForUpdate, Event clubEvent, bool trackChanges);
        //Task<List<Event>> GetAllEventsAsync(bool trackChanges);
        Task<List<Event>> GetAllEventsWithDetailsAsync(bool trackChanges);



        Task<(LinkResponse linkResponse, MetaData metaData)> GetPendingApprovalEventsAsync(LinkParameters linkParameters, bool trackChanges);

        Task<(LinkResponse linkResponse, MetaData metaData)> GetApprovedEventsAsync(LinkParameters linkParameters, bool trackChanges);

        Task<(LinkResponse linkResponse, MetaData metaData)> GetEventsByClubIdAsync(int clubId, LinkParameters linkParameters, bool trackChanges);

        Task ApproveEventAsync(int id, string approvedByUserId, bool trackChanges);


    }
}
