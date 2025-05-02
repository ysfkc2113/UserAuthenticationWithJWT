using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;


namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IEventService _eventService;
        private readonly IClubService _clubService;
        private readonly IAuthenticationService _authenticationService;

        public ServiceManager(IEventService eventService, 
            IClubService clubService, 
            IAuthenticationService authenticationService)
        {
            _eventService = eventService;
            _clubService = clubService;
            _authenticationService = authenticationService;
        }

        public IEventService EventService => _eventService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public IClubService ClubService => _clubService;
    }
}
