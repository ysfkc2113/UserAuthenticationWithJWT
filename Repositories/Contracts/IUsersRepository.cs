using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IUsersRepository:IRepositoryBase<User>
    {
        Task<PagedList<AdminUsersDto>> GetAllUsersAsync(UsersParameters usersParameters, bool trackChanges);
        Task DeleteAsync(string userName, bool trackChanges);
        Task<User> GetUserByName( string userName, bool trackChanges);
        Task UpdateActivityAsync(User user,bool trackChanges);
    }
}
