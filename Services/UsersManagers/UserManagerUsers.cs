using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.AdminService;
using Services.Contracts.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.UsersManagers
{
    public class UserManagerUsers: IUserServiceUsers
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IClubService _clubService;
        private readonly UserManager<User> _userManager;

        public UserManagerUsers(IRepositoryManager repositoryManager, IMapper mapper, IClubService clubService, UserManager<User> userManager)
        {
            _manager = repositoryManager;
            _mapper = mapper;
            _clubService = clubService;
            _userManager = userManager;
        }

        public async Task<UserDtoForMember> GetMyProfilAsync(HttpContext httpContext, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var user= await _userCheckExistAsync(userName, trackChanges);
            var userprofil= _mapper.Map<UserDtoForMember>(user);
            var roles = await _userManager.GetRolesAsync(user);
            userprofil.RoleNames = roles.ToList();
            return userprofil;
        }

        public async Task<UserDtoForMember> UpdateUserProfilAsync(HttpContext httpContext, UserDtoForMember userDtoForMember, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var member = await _userCheckExistAsync(userName, true);
            _mapper.Map(userDtoForMember, member);
            await _userManager.UpdateAsync(member);
            await _manager.SaveAsync();
            return _mapper.Map<UserDtoForMember>(member);
        }

        private async Task<string> GetUserNameByHttpContextAsync(HttpContext httpContext)
        {
            // 2. Get the User from HttpContext
            var user = httpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // 3. Get the User's ID from the JWT token's claims
            //var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("User ID claim is missing in the JWT token.");
            }
            return userName;
        }
        private async Task<User> _userCheckExistAsync(string userName, bool trackChanges)
        {
            var user = await _manager.UsersRepository.GetUserByName(userName, trackChanges);
            if (user is null)
            {
                throw new UserNotFoundException(userName);
            }
            return user;
        }
    }
}
