using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.Member
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    //[Authorize(Roles = "User")]
    [Route("api/user/profil")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProfilController:ControllerBase
    {

        private readonly IServiceManager _manager;

        public ProfilController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetMyProfil()
        {
            var myprofil=await _manager.UserServiceUsers.GetMyProfilAsync(HttpContext, false);
            if (myprofil == null) 
            {
                return BadRequest(); 
            }
            return Ok(myprofil);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ProfilGuncelleme([FromBody] UserDtoForMember userDtoForMember) 
        {
            var userProfil= await _manager.UserServiceUsers.UpdateUserProfilAsync(HttpContext, userDtoForMember, true);
            if (userProfil is not null)
            {
                return Ok(userProfil);
            }
            return BadRequest();
        }
        

    }
}
