    using Entities.Models;
    using System.Reflection;
    using System.Text;
    using System.Linq.Dynamic.Core;

    namespace Repositories.EFCore.Extensions
    {
        public static class EventRepositoryExtensions
        {
            public static IQueryable<Event> FilterEvents(this IQueryable<Event> events,
                        DateTime? startDate, DateTime? endDate, bool? isApproved, int? clubId)
            {
                // Tarih aralığı filtresini uygulayın (varsa)
                // Bu filtreleme tüm senaryolarda ortak olduğu için ilk başta yapılabilir.
                if (startDate.HasValue)
                {
                    events = events.Where(e => e.EventDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    events = events.Where(e => e.EventDate <= endDate.Value);
                }

                // Onay durumu filtresini uygulayın (varsa)
                // 'isApproved.HasValue' kontrolü, null olmayan (true veya false) tüm durumları kapsar.
                if (isApproved.HasValue)
                {
                    events = events.Where(e => e.IsApproved == isApproved.Value);
                }

                // Kulüp ID filtresini uygulayın (varsa)
                if (clubId.HasValue)
                {
                    events = events.Where(e => e.ClubId == clubId.Value);
                }

                return events; // Filtrelenmiş IQueryable nesnesini döndür
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
