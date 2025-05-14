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
    [Route("api/admin/users")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsersController : ControllerBase

    {
        private readonly IServiceManager _manager;

        public UsersController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllUsers([FromQuery]UsersParameters usersParameters)
        {
            var result=await _manager.UsersService.GetAllUsersAsync(usersParameters, false);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.adminUsersDto);

        }
        [HttpDelete]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> DeleteUser([FromBody] string userName)
        {
            await _manager.UsersService.DeleteUsersAsync(userName, true);
            return Ok();
        }
        //delete edilmişleri true ya çevirir.
        [HttpPut]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> UserActivated([FromBody]string userName)
        {
           await _manager.UsersService.UserActivatedAsync(userName, true);
            return Ok();
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = await _manager
                .AuthenticationService
                .RegisterUser(userForRegistrationDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }
        //bir kullanıcıya detaylı bilgilendirme eksik.

    }
}
