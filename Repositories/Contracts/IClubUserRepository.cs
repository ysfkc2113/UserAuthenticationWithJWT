using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IClubUserRepository:IRepositoryBase<Club_User>
    {
        //admin
        Task<PagedList<Club_User>> GetAllUsersByClubIdAsync(int id, ClubUserParameters clubUserParameters, bool trackChanges);
        Task<PagedList<Club_User>> GetAllUsersAsync(ClubUserParameters clubUserParameters, bool trackChanges);
        Task<Club_User> GetOneClubUserByIdAsync(int id, bool trackChanges);
        Task ChangeApprovedClubUserAsync(Club_User clubUser);
        void CreateClubUser(string UserId, AdminClubUserDtoInsertion adminClubUserDtoInsertion, bool trackChanges);
        void DeleteClubUser(Club_User clubUser);


        //academician
        
        Task<PagedList<Club_User>> GetAllUsersByClubIdForAcademicianAsync
                (int id, UsersParametersAcademician usersParametersAcademician, bool trackChanges);

        void CreateClubUserForAcademician(string UserId, AcademicianClubUserDtoInsertion  academicianClubUserDtoInsertion,int ClubId, bool trackChanges);
        //ClubManager
        Task<PagedList<Club_User>> GetAllUsersByClubIdForClubManagerAsync
              (int id, UsersParametersClubManager usersParametersClubManager, bool trackChanges);
        //Users
        void CreateClubUserForUsers(string UserId, int clubId, bool trackChanges);
        Task<List<Club_User>> GetClubsByUserIdAsync(string id, bool trackChanges);
        Task<PagedList<Club_User>> GetMyClubsByUserIdAsync(string id, ClubUserParameters clubUserParameters, bool trackChanges);


    }
}
