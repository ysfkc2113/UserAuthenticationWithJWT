using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IClubRepository: IRepositoryBase<Club>
    {  //Admin
       
        Task<PagedList<Club>> GetAllClubsAsync(ClubParameters clubParameters,bool trackChanges);
        Task<Club> GetOneClubByIdAsync(int id, bool trackChanges);
        void CreateClub(Club club);
        void UpdateClub(Club club);
        void DeleteClub (Club club);   

    }
}
