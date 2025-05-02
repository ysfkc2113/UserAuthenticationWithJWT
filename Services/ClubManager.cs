using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClubManager : IClubService
    {
        private readonly IRepositoryManager _manager;

        public ClubManager(IRepositoryManager repositoryManager)
        {
            _manager = repositoryManager;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges)
        {
            return await _manager.Club.GetAllClubsAsync(trackChanges);
        }

        public async Task<Club> GteOneClubByIdAsync(int id, bool trackChanges)
        {
            var club = await _manager.Club.GetOneClubByIdAsync(id, trackChanges);
            if (club == null)
            {
              throw new ClubNotFoundException(id);
            }
            return club;
           
        }
    }
}
