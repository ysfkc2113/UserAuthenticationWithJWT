using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IClubRepository Club {  get; }
        IEventRepository Event { get; }
        IClubUserRepository ClubUser { get; }
        IUsersRepository UsersRepository { get; }
        Task SaveAsync();
    }
}
