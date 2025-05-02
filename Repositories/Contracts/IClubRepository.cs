using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IClubRepository: IRepositoryBase<Club>
    {
        Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges);
        Task<Club> GetOneClubByIdAsync(int id, bool trackChanges);

        void CreateOneClub(Club club);
        void UpdateOneClub(Club club);
        void DeleteOneClub (Club club);

    }
}
