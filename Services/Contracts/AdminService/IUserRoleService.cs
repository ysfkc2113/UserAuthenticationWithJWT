using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserRoleService
    {
        Task<List<string>> GetRolesByUserIdAsync(string userName);
        Task AssignRoleToUserAsync(string userName, string roleName);
        Task RemoveRoleFromUserAsync(string userName, string roleName);
        Task<IList<string>> GetAllRolesAsync();
    }
}
