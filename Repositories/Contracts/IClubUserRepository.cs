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
        Task<PagedList<Club_User>> GetAllUsersByClubIdAsync(int id, ClubUserParameters clubUserParameters, bool trackChanges);
        Task<PagedList<Club_User>> GetAllUsersAsync(ClubUserParameters clubUserParameters, bool trackChanges);
        Task<Club_User> GetOneClubUserByIdAsync(int id, bool trackChanges);
        Task ChangeApprovedClubUserAsync(Club_User clubUser);
        void CreateClubUser(string UserId, AdminClubUserDtoInsertion adminClubUserDtoInsertion, bool trackChanges);
        void DeleteClubUser(Club_User clubUser);
    }
}
