using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.ClubLeaderService
{
    public interface IClubUserServiceClubLeader
    {
        Task<(IEnumerable<AdminClubUserDtoRelations> club_Users, MetaData metaData)>
           GetAllUsersByClubIdForClubManagerAsync(ClubUserParameters clubUserParameters, HttpContext httpContext, bool trackChanges);
        Task ChangeApprovedForClubManagerAsync(int id, HttpContext httpContext, bool trackChanges);
        Task ChangeClubRoleForClubManagerAsync(AdminClubUserDtoChangeRole adminClubUserDtoChangeRole, HttpContext httpContext, bool trackChanges);

    }
}
