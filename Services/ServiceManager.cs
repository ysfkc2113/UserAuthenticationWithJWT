using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.AcademcianService;


namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IEventService _eventService;
        private readonly IClubService _clubService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IClubUserService _clubUserService;
        private readonly IUsersService _usersService;
        private readonly IUserRoleService _userRoleService;
        private readonly IEventServiceAcademician _eventServiceAcademician;
        private readonly IClubServiceAcademician _clubServiceAcademician;
        private readonly IUsersServiceAcademician  _usersServiceAcademician;
        private readonly IClubUserServiceAcademician  _clubUserServiceAcademician;


        public ServiceManager(IEventService eventService,
            IClubService clubService,
            IAuthenticationService authenticationService,
            IClubUserService clubUserService,
            IUsersService usersService,
            IUserRoleService userRoleService,
            IEventServiceAcademician eventServiceAcademician,
            IClubServiceAcademician clubServiceAcademician,
            IUsersServiceAcademician usersServiceAcademician,
            IClubUserServiceAcademician clubUserServiceAcademician)
        {
            _eventService = eventService;
            _clubService = clubService;
            _authenticationService = authenticationService;
            _clubUserService = clubUserService;
            _usersService = usersService;
            _userRoleService = userRoleService;
            _eventServiceAcademician = eventServiceAcademician;
            _clubServiceAcademician = clubServiceAcademician;
            _usersServiceAcademician = usersServiceAcademician;
            _clubUserServiceAcademician = clubUserServiceAcademician;
        }

        public IEventService EventService => _eventService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public IClubService ClubService => _clubService;
        public IClubUserService ClubUserService => _clubUserService;
        public IUsersService UsersService => _usersService;
        public IUserRoleService UserRoleService => _userRoleService;

        //academician
        public IEventServiceAcademician  EventServiceAcademician => _eventServiceAcademician;
        public IClubServiceAcademician  ClubServiceAcademician => _clubServiceAcademician;
        public IClubUserServiceAcademician  ClubUserServiceAcademician => _clubUserServiceAcademician;
        public IUsersServiceAcademician UsersServiceAcademician => _usersServiceAcademician;
    }
}
