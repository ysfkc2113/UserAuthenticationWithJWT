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
        private readonly IClubRepository _club;

        public RepositoryManager(RepositoryContext context, IEventRepository eventRepository, IClubRepository club)
        {
            _context = context;
            _eventRepository = eventRepository;
            _club = club;
        }

        public IEventRepository Event => _eventRepository;
        public IClubRepository Club => _club;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
