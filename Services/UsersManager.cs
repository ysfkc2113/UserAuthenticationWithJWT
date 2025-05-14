using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsersManager : IUsersService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
       

        public UsersManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _manager = repositoryManager;
            _mapper = mapper;
        }

        public async Task DeleteUsersAsync(string userName, bool trackChanges)
        {
            var activite = await CheckUserActiviteAsync(userName,trackChanges);
            if (activite == false) {
                throw new UserNotFoundException(userName);
            }
            await _manager.UsersRepository.DeleteAsync(userName, trackChanges);
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<AdminUsersDto> adminUsersDto, MetaData metaData)> GetAllUsersAsync(UsersParameters usersParameters, bool trackChanges)
        {
            var users= await _manager.UsersRepository.GetAllUsersAsync(usersParameters, trackChanges);
            return (users, metaData: users.MetaData);
        }

       
        public async Task<bool> CheckUserActiviteAsync(string userName,bool trackChanges)
        {
            var user = await _userCheckExistAsync(userName,trackChanges);
            return user.IsActive;
        }
        private async Task<User> _userCheckExistAsync(string userName, bool trackChanges)
        {
            var user = await _manager.UsersRepository.GetUserByName(userName, trackChanges);
            if (user is null)
            {
                throw new UserNotFoundException(userName);
            }
            return user;
        }
        public async Task UserActivatedAsync(string userName, bool trackChanges)
        {
           var user= await _userCheckExistAsync(userName, trackChanges);
           await _manager.UsersRepository.UpdateActivityAsync(user,trackChanges);
           await _manager.SaveAsync();


        }

    }
}
