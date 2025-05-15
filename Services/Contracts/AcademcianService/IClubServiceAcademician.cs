using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.AcademcianService
{
    public interface IClubServiceAcademician
    {
        Task<ClubDto>GetOneClubByIdForAcademicianAsync( HttpContext httpContext,bool trackChanges);
        Task<ClubDto>UpdateClubForAcademicianAsync(AdminClubDtoForUpdate adminClubDtoForUpdate, HttpContext httpContext,bool trackChanges);
    }
}
