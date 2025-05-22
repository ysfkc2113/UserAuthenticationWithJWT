using AutoMapper;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Repositories.Contracts;
using Services.Contracts.ClubLeaderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.ClubLeaderManagers
{

    public class ClubManagerClubLeader: IClubServiceClubLeader
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ClubManagerClubLeader(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<ClubDto> GetOneClubByIdForClubManagerAsync(HttpContext httpContext, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByClubManagerName(userName, trackChanges);
            if (club == null)
            { throw new Exception("Bad Request"); }
            return _mapper.Map<ClubDto>(club);
        }

        public async Task<ClubDto> UpdateClubForClubManagerAsync
            (ClubManagerDtoForUpdate clubManagerDtoForUpdate, HttpContext httpContext, bool trackChanges)
        {
            var userName = await GetUserNameByHttpContextAsync(httpContext);
            var club = await _manager.Club.GetOneClubByClubManagerName(userName, trackChanges);
            if (club == null || (club.ClubId != clubManagerDtoForUpdate.ClubId))
            {
                throw new Exception("Bad Request");
            }
            var updated_club = _mapper.Map(clubManagerDtoForUpdate, club);
            _manager.Club.Update(updated_club);
            await _manager.SaveAsync();
            return _mapper.Map<ClubDto>(updated_club);

        }

        private async Task<string> GetUserNameByHttpContextAsync(HttpContext httpContext)
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
            return userName;
        }

    }
}
