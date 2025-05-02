using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges);
        Task<Club> GteOneClubByIdAsync(int id,bool trackChanges);

    }
}
