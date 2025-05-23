using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUsersService
    {
        Task<(IEnumerable<AdminUsersDto> adminUsersDto, MetaData metaData)> GetAllUsersAsync(UsersParameters usersParameters, bool trackChanges);
        Task DeleteUsersAsync(string userName,bool trackChanges);
        Task UserActivatedAsync(string userName,bool trackChanges);


        Task<string> GetUserNameByHttpContextAsync(HttpContext httpContext);
    }
}
