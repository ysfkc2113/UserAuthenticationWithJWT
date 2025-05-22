using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.ClubLeaderService
{
    public interface IClubServiceClubLeader
    {
        Task<ClubDto> GetOneClubByIdForClubManagerAsync(HttpContext httpContext, bool trackChanges);
        Task<ClubDto> UpdateClubForClubManagerAsync(ClubManagerDtoForUpdate clubManagerDtoForUpdate, HttpContext httpContext, bool trackChanges);


    }
}
