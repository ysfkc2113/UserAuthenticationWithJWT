using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class ClubUserRepositoryExtension
    {
        public static IQueryable<Club_User> FilterClubUser(this IQueryable<Club_User> clubUsers, bool? isApproved)
        {
            if (isApproved is null)
            {
                return clubUsers;
            }
            return clubUsers.Where(clubUsers => clubUsers.Approved == isApproved);
        }
        public static IQueryable<Club_User> SearchClubUser(this IQueryable<Club_User> clubs,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return clubs;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return clubs
                .Where(b => (b.User.FirstName + b.User.LastName)
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<Club_User> SortClubUser(this IQueryable<Club_User> clubs,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return clubs.OrderByDescending(b => b.User.FirstName);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Club_User>(orderByQueryString);

            if (orderQuery is null)
                return clubs.OrderByDescending(b => b.User.FirstName);

            return clubs.OrderBy(orderQuery);
        }
    }
}

