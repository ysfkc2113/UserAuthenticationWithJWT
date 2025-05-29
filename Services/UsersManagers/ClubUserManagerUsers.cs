using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.UsersManagers
{
    public class ClubUserManagerUsers : IClubUserServiceUsers
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public ClubUserManagerUsers(IRepositoryManager manager, IMapper mapper, UserManager<User> userManager)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task CreateClubUserForUsersAsync
            ( int id, HttpContext httpContext, bool trackChanges)
        {

            var user_club = await GetUserNameByHttpContextAsync(httpContext);
            var club= await _manager.Club.GetOneClubByIdAsync(id,false);
            if (club is null)
                throw new Exception("Kulüp bulunamadı.");
            var my_clubs= await _manager.ClubUser.GetClubsByUserIdAsync(user_club.Id,false);
            var varmi= my_clubs.Find(x => x.ClubId.Equals(club.ClubId));
            if (varmi is not null)
                throw new Exception("Zaten üyeliğiniz bulunmaktadır.");
            _manager.ClubUser.CreateClubUserForUsers(user_club.Id, id, trackChanges);
            await _manager.SaveAsync();
        }

        public async Task DeleteClubUserForUsersAsync(int id, HttpContext httpContext, bool trackChanges)
        {

            var user_club = await GetUserNameByHttpContextAsync(httpContext);

            var clubuser = await GetOneClubUserByIdAndCheckExists(id, trackChanges);
            if (clubuser.UserId != user_club.Id)
            {
                throw new Exception("This user doesn't you.");
            }
            if (clubuser.role_in_club == "Club Manager")
            {
                throw new Exception("Klub yöneticisidir.Silinemez.Önce rol atamasını yapın.");
            }
            _manager.ClubUser.DeleteClubUser(clubuser);

            await _manager.SaveAsync();

        }

        public async Task<(IEnumerable<MemberClubUserDtoRelations> memberClubUserDtoRelations, MetaData metaData)>
            GetAllUsersByClubIdForUsersAsync(ClubUserParameters clubUserParameters,HttpContext httpContext, bool trackChanges)
        {
            var user_club = await GetUserNameByHttpContextAsync(httpContext);
            var my_clubs = await _manager.ClubUser.GetMyClubsByUserIdAsync(user_club.Id, clubUserParameters, false);
            var memberClubUserDtoRelations = _mapper.Map<List<MemberClubUserDtoRelations>>( my_clubs);
            return (memberClubUserDtoRelations, my_clubs.MetaData);
        }
        private async Task<User> GetUserNameByHttpContextAsync(HttpContext httpContext)
        {
            var user = httpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            //var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.FindFirst(ClaimTypes.Name)?.Value;
            var member= await _userManager.FindByNameAsync(userName);

            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("User ID claim is missing in the JWT token.");
            }
            return member;
        }
        private async Task<Club_User> GetOneClubUserByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.ClubUser.GetOneClubUserByIdAsync(id, trackChanges);
            if (entity is null)
                throw new ClubUserNotFoundException(id);

            return entity;
        }

    }
}
