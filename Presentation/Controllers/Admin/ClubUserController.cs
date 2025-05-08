using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers.Admin
{

    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    [Route("api/admin/clubuser")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubUserController:ControllerBase
    
    {
        private readonly IServiceManager _manager;

        public ClubUserController(IServiceManager manager)
        {
            _manager = manager;
        }



    }
}
