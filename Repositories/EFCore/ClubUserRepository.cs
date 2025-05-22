using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ClubUserRepository : RepositoryBase<Club_User>, IClubUserRepository
    {
        public ClubUserRepository(RepositoryContext context) : base(context)
        {

        }

        public void DeleteClubUser(Club_User clubUser)
        {
            Delete(clubUser);
        }
        public async Task<PagedList<Club_User>> GetAllUsersByClubIdAsync(int id, ClubUserParameters clubUserParameters, bool trackChanges)
        {
            var clubuser= await FindAllByRelation(trackChanges, e => e.Club, e => e.User)
                .Where(y=> y.ClubId == id)
                .FilterClubUser(clubUserParameters.IsApproved)
                .SearchClubUser(clubUserParameters.SearchTerm)
                .SortClubUser(clubUserParameters.OrderBy)
                .ToListAsync();
               
                return PagedList<Club_User>
                    .ToPagedList(clubuser,
                    clubUserParameters.PageNumber,
                    clubUserParameters.PageSize);   
        }
        public async Task<PagedList<Club_User>> GetAllUsersAsync(ClubUserParameters clubUserParameters, bool trackChanges)
        {
            var clubuser= await FindAllByRelation(trackChanges, e => e.Club, e => e.User)
                .FilterClubUser(clubUserParameters.IsApproved)
                .SearchClubUser(clubUserParameters.SearchTerm)
                .SortClubUser(clubUserParameters.OrderBy)
                .ToListAsync();
               
                return PagedList<Club_User>
                    .ToPagedList(clubuser,
                    clubUserParameters.PageNumber,
                    clubUserParameters.PageSize);   
        }
        public async Task<Club_User> GetOneClubUserByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public async Task ChangeApprovedClubUserAsync(Club_User clubUser)
        {
            if (clubUser.Approved == false)
            {
                clubUser.Approved = true;
                clubUser.ApprovedTime = DateTime.Now;
            }
            else
            {
                clubUser.Approved = false;
                clubUser.ApprovedTime = null;

            }

            Update(clubUser);
        }

        public void CreateClubUser(string UserId, AdminClubUserDtoInsertion adminClubUserDtoInsertion, bool trackChanges)
        {
            var clubuser= new Club_User ()
            { ClubId = adminClubUserDtoInsertion.ClubId,
              role_in_club= adminClubUserDtoInsertion.role_in_club,
              UserId =UserId ,
              CreatedTime = DateTime.Now,
              Approved=true,
              ApprovedTime= DateTime.Now,
              
            };
            Create(clubuser);
        }




        //academician
        public async Task<PagedList<Club_User>> GetAllUsersByClubIdForAcademicianAsync
            (int id, UsersParametersAcademician usersParametersAcademician, bool trackChanges)
        {
            var clubuser = await FindAllByRelation(trackChanges, e => e.Club, e => e.User)
                .Where(y => y.ClubId == id)
                //.FilterClubUser(usersParameters.IsApproved)//isActive Users tablosunda
                .SearchClubUser(usersParametersAcademician.SearchTerm)
                .SortClubUser(usersParametersAcademician.OrderBy)
                .ToListAsync();

            return PagedList<Club_User>
                .ToPagedList(clubuser,
                usersParametersAcademician.PageNumber,
                usersParametersAcademician.PageSize);
        }

        public void CreateClubUserForAcademician(string UserId, AcademicianClubUserDtoInsertion academicianClubUserDtoInsertion, int ClubId, bool trackChanges)
        {
            var clubuser = new Club_User()
            {
                ClubId = ClubId,
                role_in_club = academicianClubUserDtoInsertion.role_in_club,
                UserId = UserId,
                CreatedTime = DateTime.Now,
                Approved = true,
                ApprovedTime = DateTime.Now,

            };
            Create(clubuser);
        }
       
        //ClubManager
        public async Task<PagedList<Club_User>> GetAllUsersByClubIdForClubManagerAsync
           (int id, UsersParametersClubManager usersParametersClubManager, bool trackChanges)
        {
            var clubuser = await FindAllByRelation(trackChanges, e => e.Club,e => e.User )
                .Where(y => y.ClubId == id)
                .Where(y=> y.User.IsActive==true)
                //.FilterClubUser(usersParameters.IsApproved)//isActive Users tablosunda
                .SearchClubUser(usersParametersClubManager.SearchTerm)
                .SortClubUser(usersParametersClubManager.OrderBy)
                .ToListAsync();

            return PagedList<Club_User>
                .ToPagedList(clubuser,
                usersParametersClubManager.PageNumber,
                usersParametersClubManager.PageSize);
        }
        //Users
        public void CreateClubUserForUsers(string UserId, int clubId, bool trackChanges)
        {
            var clubuser = new Club_User()
            {
                ClubId = clubId,
                UserId = UserId,
                CreatedTime = DateTime.Now,
                Approved = false
            };
            Create(clubuser);
        }
        public async Task<List<Club_User>> GetClubsByUserIdAsync(string id, bool trackChanges) 
        {
           var clubusers= await FindAllByRelation(trackChanges,e => e.Club, e => e.User )
                .Where(y=> y.User.Id.Equals(id)).ToListAsync();
            return clubusers;
        }
        public async Task<PagedList<Club_User>> GetMyClubsByUserIdAsync(string id, ClubUserParameters clubUserParameters, bool trackChanges)
        {
            var clubuser = await FindAllByRelation(trackChanges, e => e.Club, e => e.User)
                .Where(y => y.UserId == id)
                .FilterClubUser(clubUserParameters.IsApproved)
                .SearchClubUser(clubUserParameters.SearchTerm)
                .SortClubUser(clubUserParameters.OrderBy)
                .ToListAsync();

            return PagedList<Club_User>
                .ToPagedList(clubuser,
                clubUserParameters.PageNumber,
                clubUserParameters.PageSize);
        }
    }
    
}
