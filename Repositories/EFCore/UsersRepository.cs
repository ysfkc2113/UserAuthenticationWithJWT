using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(RepositoryContext context) : base(context)
        {
        }
        //düzeltilecek
        public async Task<PagedList<AdminUsersDto>> GetAllUsersAsync(UsersParameters usersParameters, bool trackChanges)
        {
            var filteredUsers = FindAll(trackChanges)
                .FilterUsers(usersParameters.IsActive)
                .SearchUsers(usersParameters.SearchTerm)
                .SortUsers(usersParameters.OrderBy)
                .ToList();
            var userIds = filteredUsers.Select(u => u.Id).ToList();

            // 1. SQL'e çevrilebilecek kısmı önce alıyoruz
            var userRolePairs = await _context.UserRoles
                .Where(ur => userIds.Contains(ur.UserId))
                .Join(_context.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => new { ur.UserId, RoleName = r.Name })
                .ToListAsync(); // burada bitiriyoruz, sonrasında client-side

            // 2. GroupBy işlemi artık EF dışı (LINQ to Objects)
            var userRoleMap = userRolePairs
                .GroupBy(x => x.UserId)
                .ToDictionary(g => g.Key, g => g.Select(x => x.RoleName).ToList());

            var userDtos = filteredUsers.Select(u => new AdminUsersDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                ProfilPhotoPath = u.ProfilPhotoPath,
                IsActive = u.IsActive,
                CreatedTime = u.CreatedTime,
                RolesName = userRoleMap.ContainsKey(u.Id) ? userRoleMap[u.Id] : new List<string>()
            }).ToList();




            return PagedList<AdminUsersDto>.ToPagedList(
                userDtos,
                usersParameters.PageNumber,
                usersParameters.PageSize);

        }

        public async  Task DeleteAsync(string userName, bool trackChanges)
        {
            var user = await GetUserByName(userName, trackChanges);
            user.IsActive = false;
            Update(user);
        }

        public async Task<User> GetUserByName(string userName, bool trackChanges)
        {
            var user= await FindByCondition(m => m.UserName.Equals(userName), trackChanges).FirstOrDefaultAsync();
            return user;
        }

        public async Task UpdateActivityAsync(User user, bool trackChanges)
        {
            user.IsActive = true;
            Update(user);
        }
    }
}
