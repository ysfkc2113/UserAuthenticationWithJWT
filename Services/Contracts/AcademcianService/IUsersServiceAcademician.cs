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
    public interface IUsersServiceAcademician
    {
       Task<(List<AdminUsersDto> users, MetaData metaData)>GetAllUsersForAcademicianAsync
            (HttpContext httpContext, UsersParametersAcademician usersParametersAcademician, bool trackChanges);


       // Task DeleteUsersForAcademicianAsync(string userName, HttpContext httpContext, bool trackChanges);


    }
}
