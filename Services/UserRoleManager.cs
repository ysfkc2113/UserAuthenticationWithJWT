using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Exceptions;
using Repositories.Contracts;

namespace Services
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Lazy<IClubService> _clubService;
        private readonly IRepositoryManager _manager;

        public UserRoleManager(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, Lazy<IClubService> clubService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _clubService = clubService;
        }

        public async Task<List<string>> GetRolesByUserIdAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return new List<string>();

            var roles = await _userManager.GetRolesAsync(user);
            
            return roles.ToList();
        }

        public async Task AssignRoleToUserAsync(string userName, string roleName)
        {
            var user = await _userCheckExistAsync(userName);
            if (user != null && !await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            await _manager.SaveAsync();
        }

        public async Task RemoveRoleFromUserAsync(string userName, string roleName)
        {
            var user = await _userCheckExistAsync(userName);
            if (user != null && await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
            //DİKKAT döngüye girebilir.
            if(roleName == "Club Manager" )
            {// ❗ Dikkat: burada artık Lazy çözülüyor
                await _clubService.Value.UpdateClubMangerAsync(userName);
            }
            await _manager.SaveAsync();
        }

        public async Task<IList<string>> GetAllRolesAsync()
        {
            return await Task.FromResult(_roleManager.Roles.Select(r => r.Name).ToList());
        }

        private async Task<User> _userCheckExistAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                throw new UserNotFoundException(userName);
            }
            return user;
        }
    }
}
