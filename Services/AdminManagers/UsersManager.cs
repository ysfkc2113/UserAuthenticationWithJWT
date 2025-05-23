using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.AdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.AdminManagers
{
    public class UsersManager : IUsersService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IClubService _clubService;
        private readonly UserManager<User> _userManager;

        public UsersManager(IRepositoryManager repositoryManager, IMapper mapper, IClubService clubService, UserManager<User> userManager)
        {
            _manager = repositoryManager;
            _mapper = mapper;
            _clubService = clubService;
            _userManager = userManager;
        }

        public async Task DeleteUsersAsync(string userName, bool trackChanges)
        {
            var activite = await CheckUserActiviteAsync(userName,trackChanges);
            if (activite == false) {
                throw new UserNotFoundException(userName);
            }
            await _manager.UsersRepository.DeleteAsync(userName, trackChanges);
            //club Servis ClubManager waiting
            await _clubService.UpdateClubMangerAsync(userName);
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<AdminUsersDto> adminUsersDto, MetaData metaData)> GetAllUsersAsync(UsersParameters usersParameters, bool trackChanges)
        {
            var users= await _manager.UsersRepository.GetAllUsersAsync(usersParameters, trackChanges);
            return (users, metaData: users.MetaData);
        }

       
        public async Task<bool> CheckUserActiviteAsync(string userName,bool trackChanges)
        {
            var user = await _userCheckExistAsync(userName,trackChanges);
            return user.IsActive;
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
        public async Task UserActivatedAsync(string userName, bool trackChanges)
        {
           var user= await _userCheckExistAsync(userName, trackChanges);
           await _manager.UsersRepository.UpdateActivityAsync(user,trackChanges);
           await _manager.SaveAsync();


        }

        public async Task<string> GetUserNameByHttpContextAsync(HttpContext httpContext)
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
       
    }
}
