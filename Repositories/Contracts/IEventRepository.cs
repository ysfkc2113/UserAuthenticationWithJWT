using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        Task<PagedList<Event>> GetAllEventsAsync(EventParameters eventParameters,
            bool trackChanges);
        Task<Event> GetOneEventByIdAsync(int id, bool trackChanges);
        void CreateOneEvent(Event clubEvent);
        void UpdateOneEvent(Event clubEvent);
        void DeleteOneEvent(Event clubEvent);
        //Task<List<Event>> GetAllEventsAsync(bool trackChanges);//silinecek
        Task<List<Event>> GetAllEventsWithDetailsAsync(bool trackChanges);

        // New Methods
        Task<PagedList<Event>> GetEventsByClubIdAsync(int clubId, EventParameters eventParameters, bool trackChanges);
        Task<Event> GetEventByIdWithDetailsAsync(int id, bool trackChanges);
        Task<PagedList<Event>> GetApprovedEventsAsync(EventParameters eventParameters, bool trackChanges);
        Task<PagedList<Event>> GetPendingApprovalEventsAsync(EventParameters eventParameters, bool trackChanges);
        void ChangeApprovedEvent(Event clubEvent,string userId);

    }
}
