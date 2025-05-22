using Services.Contracts.AcademcianService;
using Services.Contracts.ClubLeaderService;
using Services.Contracts.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IEventService EventService { get; }
        IClubService ClubService { get; }
        IClubUserService ClubUserService { get; }
        IUsersService UsersService { get; }
        IUserRoleService UserRoleService { get; }
        IAuthenticationService AuthenticationService { get; }
        //Academician
        IEventServiceAcademician EventServiceAcademician { get; }
        IClubServiceAcademician ClubServiceAcademician { get; }
        IUsersServiceAcademician UsersServiceAcademician { get; }
        IClubUserServiceAcademician ClubUserServiceAcademician { get; }
        IUserRoleServiceAcademician UserRoleServiceAcademician { get; }
        //ClubLeader
        IEventServiceClubLeader EventServiceClubLeader { get; }
        IClubServiceClubLeader ClubServiceClubLeader { get; }
        IUsersServiceClubLeader UsersServiceClubLeader { get; }
        IClubUserServiceClubLeader ClubUserServiceClubLeader { get; }
        //Users
        IClubUserServiceUsers ClubUserServiceUsers { get; }
    }
}
