using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.UsersService
{
    public interface IUserServiceUsers
    {
        Task<UserDtoForMember> UpdateUserProfilAsync(HttpContext httpContext, UserDtoForMember userDtoForMember, bool trackChanges);
        Task<UserDtoForMember> GetMyProfilAsync(HttpContext httpContext,bool trackChanges);
    }
}
