using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class EventRepositoryExtensions
    {
        public static IQueryable<Event> FilterEvents(this IQueryable<Event> events,
            uint minPrice, uint maxPrice) =>
            events.Where(clubEvent =>
            clubEvent.Price >= minPrice &&
            clubEvent.Price <= maxPrice);

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
                return events.OrderBy(b => b.Id);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Event>(orderByQueryString);

            if (orderQuery is null)
                return events.OrderBy(b => b.Id);

            return events.OrderBy(orderQuery);
        }
    }
}
