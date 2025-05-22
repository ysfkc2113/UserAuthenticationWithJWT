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
    public interface IUsersServiceClubLeader
    {
        Task<(List<AdminUsersDto> users, MetaData metaData)> GetAllUsersForClubManagerAsync
           (HttpContext httpContext, UsersParametersClubManager usersParametersClubManager, bool trackChanges);   
    }
}
