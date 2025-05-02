using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public sealed class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext context) : base(context)
        {
            
        }

        public void CreateOneEvent(Event clubEvent) => Create(clubEvent);
        public void DeleteOneEvent(Event clubEvent) => Delete(clubEvent);
        public async Task<PagedList<Event>> GetAllEventsAsync(EventParameters eventParameters,
            bool trackChanges)
        {
            var clubEvents = await FindAll(trackChanges)
                .FilterEvents(eventParameters.MinPrice, eventParameters.MaxPrice)
                .Search(eventParameters.SearchTerm)
                .Sort(eventParameters.OrderBy)
                .ToListAsync();

            return PagedList<Event>
                .ToPagedList(clubEvents,
                eventParameters.PageNumber,
                eventParameters.PageSize);
        }

        public async Task<List<Event>> GetAllEventsAsync(bool trackChanges)
        {
            var clubEvents= await FindAll(trackChanges).OrderBy(m=> m.Id).ToListAsync();
            return clubEvents;
        }

        public async Task<List<Event>> GetAllEventsWithDetailsAsync(bool trackChanges)
        {
            return await _context.Events.Include(m=>m.Club).OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Event> GetOneEventByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        public void UpdateOneEvent(Event clubEvent) => Update(clubEvent);
    }
}
