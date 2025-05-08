using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
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
 //for admin
        public void CreateClub(Club club)=> Create(club);
        public void DeleteClub(Club club) => Delete(club);
        public void UpdateClub(Club club) => Update(club); 
        public async Task<Club> GetOneClubByIdAsync(int id, bool trackChanges)
        {
           return await FindByCondition(b => b.ClubId.Equals(id), trackChanges)
           .SingleOrDefaultAsync();
        }
        public async Task<PagedList<Club>> GetAllClubsAsync(ClubParameters clubParameters, bool trackChanges)
        {
            var clubs = await FindAll(trackChanges)
                  .Search(clubParameters.SearchTerm)
                  .Sort(clubParameters.OrderBy)
                  .ToListAsync();

            return PagedList<Club>
                .ToPagedList(clubs,
                clubParameters.PageNumber,
                clubParameters.PageSize);

        }

    }
}
