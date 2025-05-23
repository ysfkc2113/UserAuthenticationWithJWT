using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IClubUserService
    {
        Task<(IEnumerable<AdminClubUserDtoRelations> club_Users, MetaData metaData)> GetAllUsersByClubIdAsync(int id, ClubUserParameters clubUserParameters, bool trackChanges);
        Task<(IEnumerable<AdminClubUserDtoRelations> club_Users, MetaData metaData)> GetAllUsersAsync(ClubUserParameters clubUserParameters, bool trackChanges);
        Task ChangeApprovedAsync(int id, bool trackChanges);
        Task DeleteClubUserAsync(int id,bool trackChanges);
        Task ChangeClubRoleAsync(AdminClubUserDtoChangeRole adminClubUserDtoChangeRole,bool trackChanges);
        Task CreateClubUserAsync(AdminClubUserDtoInsertion adminClubUserDtoInsertion,bool trackChanges);
    }
}
