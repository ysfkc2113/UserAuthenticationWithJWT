using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IClubUserRepository _clubUserRepository;
        private readonly IUsersRepository _usersRepository;

        public RepositoryManager(RepositoryContext context, IEventRepository eventRepository, IClubRepository club, IClubUserRepository clubUserRepository, IUsersRepository usersRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
            _clubRepository = club;
            _clubUserRepository = clubUserRepository;
            _usersRepository = usersRepository;
        }

        public IEventRepository Event => _eventRepository;
        public IClubRepository Club => _clubRepository;

        public IClubUserRepository ClubUser => _clubUserRepository;
        public IUsersRepository UsersRepository => _usersRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
