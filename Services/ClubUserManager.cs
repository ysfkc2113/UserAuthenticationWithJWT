using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClubUserManager : IClubUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public ClubUserManager(IRepositoryManager manager, IMapper mapper, UserManager<User> userManager)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<(IEnumerable<AdminClubUserDtoRelations> club_Users, MetaData metaData)> GetAllUsersByClubIdAsync(int id, ClubUserParameters clubUserParameters, bool trackChanges)
        {
            var clubuser = await _manager.ClubUser.GetAllUsersByClubIdAsync(id, clubUserParameters, trackChanges);

            var  clubUserDtoWithRelations= _mapper.Map<IEnumerable<AdminClubUserDtoRelations>>(clubuser);


            return (clubUserDtoWithRelations, metaData: clubuser.MetaData);
        }
        public async Task<(IEnumerable<AdminClubUserDtoRelations> club_Users, MetaData metaData)> GetAllUsersAsync(ClubUserParameters clubUserParameters, bool trackChanges)
        {
            var clubuser = await _manager.ClubUser.GetAllUsersAsync(clubUserParameters, trackChanges);

            var  clubUserDtoWithRelations= _mapper.Map<IEnumerable<AdminClubUserDtoRelations>>(clubuser);
            return (clubUserDtoWithRelations, metaData: clubuser.MetaData);
        }
        public async Task ChangeApprovedAsync(int id,bool trackChanges)
        {
                var clubuser = await GetOneClubUserByIdAndCheckExists(id, trackChanges);
                _manager.ClubUser.ChangeApprovedClubUserAsync(clubuser);
                await _manager.SaveAsync();           
        } 
        private async Task<Club_User> GetOneClubUserByIdAndCheckExists(int id, bool trackChanges)
            { 
                var entity = await _manager.ClubUser.GetOneClubUserByIdAsync(id, trackChanges);
                if (entity is null)
                    throw new ClubUserNotFoundException(id);

                return entity;
            }

        public async Task DeleteClubUserAsync(int id, bool trackChanges)
        {
            var entity = await GetOneClubUserByIdAndCheckExists(id, trackChanges);
            _manager.ClubUser.DeleteClubUser(entity);
            if (entity.role_in_club=="Club Manager")
            {
                var club = await _manager.Club.GetOneClubByIdAsync(entity.ClubId,trackChanges);
                club.ClubManager = "Waiting";
                _manager.Club.Update(club);
            }
            await _manager.SaveAsync();
        }

        public async Task ChangeClubRoleAsync(AdminClubUserDtoChangeRole adminClubUserDtoChangeRole, bool trackChanges)
        {
            var entity = await GetOneClubUserByIdAndCheckExists(adminClubUserDtoChangeRole.Id, trackChanges);
            entity.role_in_club = adminClubUserDtoChangeRole.role_in_club;
            _manager.ClubUser.Update(entity);
            // club tablosu için
            if (entity.role_in_club == "Club Manager") 
            {
                var user= await _userManager.FindByIdAsync(entity.UserId);
                var club = await _manager.Club.GetOneClubByIdAsync(entity.ClubId, trackChanges);
                club.ClubManager=user.UserName;
                _manager.Club.Update(club);
            }
               
            await _manager.SaveAsync();
        }

        public async Task CreateClubUserAsync(AdminClubUserDtoInsertion adminClubUserDtoInsertion, bool trackChanges)
        {
            var user = await _userManager.FindByNameAsync(adminClubUserDtoInsertion.UserName);
            _manager.ClubUser.CreateClubUser(user.Id,adminClubUserDtoInsertion,trackChanges);

            if (adminClubUserDtoInsertion.role_in_club == "Club Manager")
            {           
                var club = await _manager.Club.GetOneClubByIdAsync(adminClubUserDtoInsertion.ClubId, trackChanges);
                club.ClubManager = user.UserName;
                _manager.Club.Update(club);
            }

            await _manager.SaveAsync();
        }



    

    }
}