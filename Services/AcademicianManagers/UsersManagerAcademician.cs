using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.AcademcianService;
using Services.Contracts.AdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.AcademicianManagers
{
    public class UsersManagerAcademician : IUsersServiceAcademician
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IClubService _clubService;

        public UsersManagerAcademician(IRepositoryManager repositoryManager, IMapper mapper, IClubService clubService)
        {
            _manager = repositoryManager;
            _mapper = mapper;
            _clubService = clubService;
        }

        //public async Task DeleteUsersForAcademicianAsync(string userName, HttpContext httpContext, bool trackChanges)
        //{
        //    var userName_club = await GetUserNameByHttpContextAsync(httpContext);

        //}

        public async Task<(List<AdminUsersDto> users, MetaData metaData)> GetAllUsersForAcademicianAsync
            (HttpContext httpContext, UsersParametersAcademician usersParametersAcademician, bool trackChanges)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);
            var clubUsers = await _manager.ClubUser.GetAllUsersByClubIdForAcademicianAsync
                                (userName_club.club.ClubId, usersParametersAcademician, trackChanges);
            var users = _mapper.Map<List<AdminUsersDto>>(clubUsers);
            return (users: users, metaData: clubUsers.MetaData);
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
    }
}
