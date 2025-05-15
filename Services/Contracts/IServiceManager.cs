using Services.Contracts.AcademcianService;
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
    }
}
