using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.UsersService
{
    public interface IClubUserServiceUsers
    {
        Task<(IEnumerable<AdminClubUserDtoRelations> club_Users, MetaData metaData)>
           GetAllUsersByClubIdForUsersAsync(ClubUserParameters clubUserParameters, HttpContext httpContext, bool trackChanges);
        Task DeleteClubUserForUsersAsync(int id, HttpContext httpContext, bool trackChanges);
        Task CreateClubUserForUsersAsync( int id, HttpContext httpContext, bool trackChanges);

    }
}
