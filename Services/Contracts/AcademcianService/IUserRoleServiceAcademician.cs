using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.AcademcianService
{
    public interface IUserRoleServiceAcademician
    {
        Task<List<string>> GetRolesByUserIdForAcademicianAsync(HttpContext httpContext,string userName);
        Task AssignRoleToUserForAcademicianAsync(HttpContext httpContext,string userName, string roleName);
        Task RemoveRoleFromUserForAcademicianAsync(HttpContext httpContext,string userName, string roleName);
    }
}
