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

        public void CreateOneEvent(Event clubEvent)
        {
            clubEvent.IsApproved = false;
            clubEvent.ApprovedTime = DateTime.MinValue;
            clubEvent.CreatedTime = DateTime.Now;
            Create(clubEvent); } 
        public void DeleteOneEvent(Event clubEvent) => Delete(clubEvent);
        public void UpdateOneEvent(Event clubEvent)
        {

            if (clubEvent.IsApproved && clubEvent.ApprovedTime==DateTime.MinValue) 
            {
                clubEvent.ApprovedTime = DateTime.Now;
              //herkesin onaylamaması lazım sadece admin ve hoca yapabilir.
            }
            Update(clubEvent);
        } 
        public async Task<PagedList<Event>> GetAllEventsAsync(EventParameters eventParameters,
            bool trackChanges)
        {
            var clubEvents = await FindAllByRelation(trackChanges, e => e.Club)
                .FilterEvents(eventParameters.StartDate, eventParameters.EndDate,eventParameters.IsApproved)
                .Search(eventParameters.SearchTerm)
                .Sort(eventParameters.OrderBy)
                .ToListAsync();

            return PagedList<Event>
                .ToPagedList(clubEvents,
                eventParameters.PageNumber,
                eventParameters.PageSize);
        }

        //public async Task<List<Event>> GetAllEventsAsync(bool trackChanges)//
        //{
        //    var clubEvents = await FindAllByRelation(trackChanges, e => e.Club).OrderBy(m => m.Id).ToListAsync();
        //    return clubEvents;
        //}

        public async Task<List<Event>> GetAllEventsWithDetailsAsync(bool trackChanges)//ilişkili dto haline getirilebili,r
        {
            return await FindAllByRelation(trackChanges, e => e.Club).OrderBy(m => m.Id).ToListAsync();
        }





        public async Task<Event> GetOneEventByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();



        public async Task<PagedList<Event>> GetPendingApprovalEventsAsync(EventParameters eventParameters, bool trackChanges)
        {
            var clubEvents = await FindAllByRelation(trackChanges, e => e.Club)
               //.FilterEvents(eventParameters.MinPrice, eventParameters.MaxPrice)
               .Where(y=> !y.IsApproved)
               .Search(eventParameters.SearchTerm)
               .Sort(eventParameters.OrderBy)
               .ToListAsync();

            return PagedList<Event>
                .ToPagedList(clubEvents,
                eventParameters.PageNumber,
                eventParameters.PageSize);
        }

      

        public async Task<PagedList<Event>> GetApprovedEventsAsync(EventParameters eventParameters, bool trackChanges)
        {
            var clubEvents = await FindAllByRelation(trackChanges, e => e.Club)
               //.FilterEvents(eventParameters.MinPrice, eventParameters.MaxPrice)
               .Where(y => y.IsApproved)
               .Search(eventParameters.SearchTerm)
               .Sort(eventParameters.OrderBy)
               .ToListAsync();

            return PagedList<Event>
                .ToPagedList(clubEvents,
                eventParameters.PageNumber,
                eventParameters.PageSize);
        }

        public async Task<Event> GetEventByIdWithDetailsAsync(int id, bool trackChanges)
        {
            return await FindAllByRelation(trackChanges, e => e.Club).Where(y=> y.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Event>> GetEventsByClubIdAsync(int clubId, EventParameters eventParameters, bool trackChanges)
        {
           var clubEvents = await FindByCondition(m=>m.ClubId.Equals(clubId),trackChanges)
                .Search(eventParameters.SearchTerm)
                .Sort(eventParameters.OrderBy)
                .ToListAsync();
            return PagedList<Event>
                .ToPagedList(clubEvents,
                eventParameters.PageNumber,
                eventParameters.PageSize);
        }

        public void ChangeApprovedEvent(Event clubEvent, string userId)
        {
            if (clubEvent.IsApproved == false)
            {
                clubEvent.IsApproved = true;
                clubEvent.ApprovedTime = DateTime.Now;
                clubEvent.ApprovedByUserName = userId;
            }
            else
            {
                clubEvent.IsApproved= false;
            }
            
            Update(clubEvent);
        }
    }
}
