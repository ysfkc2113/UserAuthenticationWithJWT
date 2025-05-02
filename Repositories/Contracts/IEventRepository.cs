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
        Task<List<Event>> GetAllEventsAsync(bool trackChanges);
        Task<List<Event>> GetAllEventsWithDetailsAsync(bool trackChanges);

    }
}
