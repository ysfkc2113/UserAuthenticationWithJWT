using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts.AcademcianService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.AcademicianManagers
{
    public class ClubUserManagerAcademician : IClubUserServiceAcademician
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public ClubUserManagerAcademician(IRepositoryManager manager, IMapper mapper, UserManager<User> userManager)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<(IEnumerable<AdminClubUserDtoRelations> club_Users, MetaData metaData)> GetAllUsersByClubIdForAcademicianAsync(ClubUserParameters clubUserParameters, HttpContext httpContext, bool trackChanges)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);
            var clubuser = await _manager.ClubUser.GetAllUsersByClubIdAsync
                                (userName_club.club.ClubId, clubUserParameters, trackChanges);
            var clubUserDtoWithRelations = _mapper.Map<IEnumerable<AdminClubUserDtoRelations>>(clubuser);

            return (clubUserDtoWithRelations, metaData: clubuser.MetaData);
        }
        public async Task ChangeApprovedForAcademicianAsync(int id, HttpContext httpContext, bool trackChanges)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);

            var clubuser = await GetOneClubUserByIdAndCheckExists(id, trackChanges);
            if (clubuser.ClubId != userName_club.club.ClubId)
            {
                throw new Exception("This user doesn't your club member.");
            }
            if (userName_club.club.ClubManager == "Club Manger")
                throw new Exception("Klub yöneticisidir.Onayı değiştirilemez.Önce rol atamasını yapın.");
            _manager.ClubUser.ChangeApprovedClubUserAsync(clubuser);
            await _manager.SaveAsync();
        }


        public async Task DeleteClubUserForAcademicianAsync(int id, HttpContext httpContext, bool trackChanges)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);

            var clubuser = await GetOneClubUserByIdAndCheckExists(id, trackChanges);
            if (clubuser.ClubId != userName_club.club.ClubId)
            {
                throw new Exception("This user doesn't your club member.");
            }
            if (clubuser.role_in_club == "Club Manager")
            {
                throw new Exception("Klub yöneticisidir.Silinemez.Önce rol atamasını yapın.");
            }
            _manager.ClubUser.DeleteClubUser(clubuser);

            await _manager.SaveAsync();
        }
        public async Task CreateClubUserForAcademicianAsync(AcademicianClubUserDtoInsertion academicianClubUserDtoInsertion, HttpContext httpContext, bool trackChanges)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);

            var user = await _userManager.FindByNameAsync(academicianClubUserDtoInsertion.UserName);
            _manager.ClubUser.CreateClubUserForAcademician(user.Id, academicianClubUserDtoInsertion, userName_club.club.ClubId, trackChanges);

            if (academicianClubUserDtoInsertion.role_in_club == "Club Manager")
            {
                var club = await _manager.Club.GetOneClubByIdAsync(userName_club.club.ClubId, trackChanges);
                if (club.ClubManager != null)
                {
                    throw new Exception("This club already has a ClubManager");
                }
                club.ClubManager = user.UserName;
                if (!await _userManager.IsInRoleAsync(user, academicianClubUserDtoInsertion.role_in_club))
                {
                    await _userManager.AddToRoleAsync(user, academicianClubUserDtoInsertion.role_in_club);
                }
                _manager.Club.Update(club);
            }
            await _manager.SaveAsync();
        }

        public async Task ChangeClubRoleForAcademicianAsync(AdminClubUserDtoChangeRole adminClubUserDtoChangeRole, HttpContext httpContext, bool trackChanges)
        {
            var userName_club = await GetUserNameByHttpContextAsync(httpContext);

            var clubuser = await GetOneClubUserByIdAndCheckExists(adminClubUserDtoChangeRole.Id, trackChanges);
            if (clubuser.ClubId != userName_club.club.ClubId)
            {
                throw new Exception("This user doesn't your club member.");
            }

            clubuser.role_in_club = adminClubUserDtoChangeRole.role_in_club;
            var user = await _userManager.FindByIdAsync(clubuser.UserId);
            if (user == null || user.IsActive == false)
            { throw new Exception("Kayıtlı kullanıcı bulunamadı."); }
            //rütbe düşürme
            if (userName_club.club.ClubManager==user.UserName && adminClubUserDtoChangeRole.role_in_club != "Club Manager")
            {
                throw new Exception("Klub yöneticisidir.Buradan Rol ataması yapılamaz.Önce rol atamasını yapın.");

            }
            // rütbe yükseltme
            if (clubuser.role_in_club == "Club Manager")
            {
              
                if ((userName_club.club.ClubManager != "Waiting") && (userName_club.club.ClubManager != null))
                    throw new Exception("Bu kulünün mevcut bir yöneticisi vardır.");
                userName_club.club.ClubManager = user.UserName;
                _manager.Club.Update(userName_club.club);
                //role tablosunada kayıt edilecek
                if (!await _userManager.IsInRoleAsync(user, adminClubUserDtoChangeRole.role_in_club))
                {
                    await _userManager.AddToRoleAsync(user, adminClubUserDtoChangeRole.role_in_club);
                }
            }
            _manager.ClubUser.Update(clubuser);

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
    }
}
