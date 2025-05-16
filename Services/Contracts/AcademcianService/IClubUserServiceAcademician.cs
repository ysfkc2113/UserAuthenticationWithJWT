using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.AcademcianService
{
    public interface IClubUserServiceAcademician
    {
        Task<(IEnumerable<AdminClubUserDtoRelations> club_Users, MetaData metaData)> 
            GetAllUsersByClubIdForAcademicianAsync(ClubUserParameters clubUserParameters, HttpContext httpContext ,bool trackChanges);
        Task ChangeApprovedForAcademicianAsync(int id,HttpContext httpContext, bool trackChanges);
        Task DeleteClubUserForAcademicianAsync(int id, HttpContext httpContext, bool trackChanges);
        Task ChangeClubRoleForAcademicianAsync(AdminClubUserDtoChangeRole adminClubUserDtoChangeRole,HttpContext httpContext, bool trackChanges);
        Task CreateClubUserForAcademicianAsync(AcademicianClubUserDtoInsertion  academicianClubUserDtoInsertion,HttpContext httpContext, bool trackChanges);
    }
}
