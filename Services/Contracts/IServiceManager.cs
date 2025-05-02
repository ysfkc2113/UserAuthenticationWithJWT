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
        IAuthenticationService AuthenticationService { get; }
    }
}
