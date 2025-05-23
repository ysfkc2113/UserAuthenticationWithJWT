using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.AcademcianService;
using Services.Contracts.AdminService;
using Services.Contracts.ClubLeaderService;
using Services.Contracts.UsersService;


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
        private readonly IUserRoleServiceAcademician _userRoleServiceAcademician;
        private readonly IEventServiceClubLeader _eventServiceClubLeader;
        private readonly IClubServiceClubLeader _clubServiceClubLeader;
        private readonly IUsersServiceClubLeader _usersServiceClubLeader;
        private readonly IClubUserServiceClubLeader _clubUserServiceClubLeader;
        private readonly IClubUserServiceUsers _clubUserServiceUsers;
        private readonly IUserServiceUsers _userServiceUsers;
        private readonly IEventServiceUsers _eventServiceUsers;
        private readonly IClubServiceUsers _clubServiceUsers;


        public ServiceManager(IEventService eventService,
            IClubService clubService,
            IAuthenticationService authenticationService,
            IClubUserService clubUserService,
            IUsersService usersService,
            IUserRoleService userRoleService,
            IEventServiceAcademician eventServiceAcademician,
            IClubServiceAcademician clubServiceAcademician,
            IUsersServiceAcademician usersServiceAcademician,
            IClubUserServiceAcademician clubUserServiceAcademician,
            IUserRoleServiceAcademician userRoleServiceAcademician,
            IEventServiceClubLeader eventServiceClubLeader,
            IClubServiceClubLeader clubServiceClubLeader,
            IUsersServiceClubLeader usersServiceClubLeader,
            IClubUserServiceClubLeader clubUserServiceClubLeader,
            IClubUserServiceUsers clubUserServiceUsers,
            IUserServiceUsers userServiceUsers,
            IEventServiceUsers eventServiceUsers,
            IClubServiceUsers clubServiceUsers)
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
            _userRoleServiceAcademician = userRoleServiceAcademician;
            _eventServiceClubLeader = eventServiceClubLeader;
            _clubServiceClubLeader = clubServiceClubLeader;
            _usersServiceClubLeader = usersServiceClubLeader;
            _clubUserServiceClubLeader = clubUserServiceClubLeader;
            _clubUserServiceUsers = clubUserServiceUsers;
            _userServiceUsers = userServiceUsers;
            _eventServiceUsers = eventServiceUsers;
            _clubServiceUsers = clubServiceUsers;
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
        public IUserRoleServiceAcademician UserRoleServiceAcademician=> _userRoleServiceAcademician;
        //ClubLeader
        public IEventServiceClubLeader EventServiceClubLeader => _eventServiceClubLeader;
        public IClubServiceClubLeader ClubServiceClubLeader => _clubServiceClubLeader;
        public IUsersServiceClubLeader UsersServiceClubLeader => _usersServiceClubLeader;
        public IClubUserServiceClubLeader ClubUserServiceClubLeader => _clubUserServiceClubLeader;
        //Users
        public IClubUserServiceUsers ClubUserServiceUsers => _clubUserServiceUsers;

        public IUserServiceUsers UserServiceUsers => _userServiceUsers;

        public IEventServiceUsers EventServiceUsers => _eventServiceUsers;

        IClubServiceUsers IServiceManager.ClubServiceUsers => _clubServiceUsers;
    }
}
