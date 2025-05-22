using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.AcademcianService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.AcademicianManagers
{
    public class UserRoleManagerAcademician : IUserRoleServiceAcademician
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Lazy<IClubService> _clubService;
        private readonly IRepositoryManager _manager;

        public UserRoleManagerAcademician(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, Lazy<IClubService> clubService, IRepositoryManager manager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _clubService = clubService;
            _manager = manager;
        }

        public async Task<List<string>> GetRolesByUserIdForAcademicianAsync(HttpContext httpContext,string userName)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);
            var user = await _userCheckExistAsync(userName);
            if (user == null)
            {
                throw new UserNotFoundException(userName);
            }
            var usersclub= _manager.ClubUser.FindByCondition(y=> y.UserId.Equals(user.Id),true).SingleOrDefault();
            if (usersclub.ClubId != userName_club.club.ClubId)
            {
                throw new Exception("Bu user sizin Club a ait değildir.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        public async Task AssignRoleToUserForAcademicianAsync(HttpContext httpContext, string userName, string roleName)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);
            var user = await _userCheckExistAsync(userName);
            var usersclub = _manager.ClubUser.FindByCondition(y => y.UserId.Equals(user.Id), true).SingleOrDefault();
            if (user != null && !await _userManager.IsInRoleAsync(user, roleName))
            {
                if (roleName == "Club Manager" && userName_club.club.ClubManager == "Waiting")
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                    userName_club.club.ClubManager = userName;
                }
                else if (roleName == "Club Manager" && userName_club.club.ClubManager == null)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                    userName_club.club.ClubManager = userName;
                }
                else if (roleName != "Club Manager")
                    throw new Exception("Bu alan için sadece 'Club Manager' rolü atayabilirsiniz.");

                else { throw new Exception("Bu Club ın zaten bir yöneticisi var önce yöneticinin rolünü değiştirin."); }
            }
            await _manager.SaveAsync();
        }
        public async Task RemoveRoleFromUserForAcademicianAsync(HttpContext httpContext, string userName, string roleName)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);
            var user = await _userCheckExistAsync(userName);
            var usersclub = _manager.ClubUser.FindByCondition(y => y.UserId.Equals(user.Id), true).SingleOrDefault();
            if (usersclub.ClubId != userName_club.club.ClubId)
                throw new Exception("Bu User sizin Cluba ait değil.");
            if (user != null && await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
            //DİKKAT döngüye girebilir.
            if (roleName == "Club Manager")
            {// ❗ Dikkat: burada artık Lazy çözülüyor
                await _clubService.Value.UpdateClubMangerAsync(userName);
            }
            await _manager.SaveAsync();

        }





        private async Task<(string userName, Club club)> GetUserNameByHttpContextAsync(HttpContext httpContext)
        {
            var user = httpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            //var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("User ID claim is missing in the JWT token.");
            }

            var club = await _manager.Club.GetOneClubByAcademicianName(userName, false);
            if (club == null) { throw new Exception("Her hangi bir külübe danışman değilsiniz."); }
            return (userName, club);
        }
        private async Task<Club_User> GetOneClubUserByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.ClubUser.GetOneClubUserByIdAsync(id, trackChanges);
            if (entity is null)
                throw new ClubUserNotFoundException(id);

            return entity;
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
