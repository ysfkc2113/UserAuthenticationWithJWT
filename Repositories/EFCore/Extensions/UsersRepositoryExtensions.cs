using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class UsersRepositoryExtensions
    {
        public static IQueryable<User> FilterUsers(this IQueryable<User> users, bool? IsActive)
        {
            if (IsActive is null)
            {
                return users;
            }
            return users.Where(m => m.IsActive == IsActive);
        }
        public static IQueryable<User> SearchUsers(this IQueryable<User> users,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return users;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return users
                .Where(b => b.FirstName
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<User> SortUsers(this IQueryable<User> users,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return users.OrderByDescending(b => b.UserName);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<User>(orderByQueryString);

            if (orderQuery is null)
                return users.OrderByDescending(b => b.UserName);

            return users.OrderBy(orderQuery);
        }
    }
}

