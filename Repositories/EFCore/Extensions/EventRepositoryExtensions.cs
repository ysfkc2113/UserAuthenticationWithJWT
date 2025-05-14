using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class EventRepositoryExtensions
    {
        public static IQueryable<Event> FilterEvents(this IQueryable<Event> events,
            DateTime? startDate, DateTime? endDate, bool? isApproved,int? clubId)
        {
            if (isApproved is null && clubId == null)
            {
                return events.Where(clubEvent =>
               (clubEvent.EventDate >= startDate &&
               clubEvent.EventDate <= endDate));
            }
            else if (isApproved is null && clubId != null)
            {
                return events.Where(clubEvent =>
               (clubEvent.EventDate >= startDate &&
               clubEvent.EventDate <= endDate)&& clubEvent.ClubId == clubId);
            }
            else if ((!isApproved is null) && clubId == null)
            {
                return events.Where(clubEvent =>
               (clubEvent.EventDate >= startDate &&
               clubEvent.EventDate <= endDate)&& clubEvent.IsApproved == isApproved);
            }

            return events.Where(clubEvent =>
            (clubEvent.EventDate >= startDate &&
            clubEvent.EventDate <= endDate) && clubEvent.IsApproved == isApproved && clubEvent.ClubId==clubId);
        }


        public static IQueryable<Event> Search(this IQueryable<Event> events,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return events;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return events
                .Where(b => b.Title
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<Event> Sort(this IQueryable<Event> events,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return events.OrderByDescending(b => b.CreatedTime);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Event>(orderByQueryString);

            if (orderQuery is null)
                return events.OrderByDescending(b => b.CreatedTime);

            return events.OrderBy(orderQuery);
        }
    }
}
