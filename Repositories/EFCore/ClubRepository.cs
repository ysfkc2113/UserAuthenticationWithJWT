using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ClubRepository : RepositoryBase<Club>, IClubRepository
    {
        public ClubRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneClub(Club club)=> Create(club);

        public void DeleteOneClub(Club club) => Delete(club);
        public void UpdateOneClub(Club club) => Update(club);

        public async Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges)
        {
            var clubs = await FindAll(trackChanges).OrderBy(y=> y.ClubName).ToListAsync();
            return clubs;
        }

        public async Task<Club> GetOneClubByIdAsync(int id, bool trackChanges)
        {
           return await FindByCondition(b => b.ClubId.Equals(id), trackChanges)
           .SingleOrDefaultAsync();
        }


    }
}
