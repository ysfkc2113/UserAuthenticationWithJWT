using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class ClubRepositoryExtensions
    {
        public static IQueryable<Club> FilterClub(this IQueryable<Club> club,string? Faculty)
        {
            if (Faculty is null)
            {
                return club;
            }
            return club.Where(club => club.Faculty == Faculty);
        }
        public static IQueryable<Club> SearchClub(this IQueryable<Club> clubs,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return clubs;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return clubs
                .Where(b => b.ClubName
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<Club> SortClub(this IQueryable<Club> clubs,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return clubs.OrderByDescending(b => b.ClubName);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Club>(orderByQueryString);

            if (orderQuery is null)
                return clubs.OrderByDescending(b => b.ClubName);

            return clubs.OrderBy(orderQuery);
        }
    }
}

