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

namespace Presentation.Controllers.Academician
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    // [Authorize(Roles = "Academician")]
    [Route("api/academician/users")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsersController: ControllerBase
    {
        private readonly IServiceManager _manager;

        public UsersController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllUsers([FromQuery] UsersParametersAcademician  usersParametersAcademician)
        {
            var result = await _manager.UsersServiceAcademician.GetAllUsersForAcademicianAsync(HttpContext, usersParametersAcademician, false);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.users);
        }



        //club den silme işi club_user controllerın işi?
        //[HttpDelete]
        //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //public async Task<IActionResult> DeleteUser([FromBody] string userName)
        //{
        //    await _manager.UsersServiceAcademician.DeleteUsersForAcademicianAsync(userName,HttpContext, true);

        //    return Ok();
        //}

        //delete edilmişleri true ya çevirir.
        //[HttpPut]
        //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //public async Task<IActionResult> UserActivated([FromBody] string userName)
        //{
        //    await _manager.UsersService.UserActivatedAsync(userName, true);
        //    return Ok();
        //}


        //academician bir user kayıt etsin mi?
        //[HttpPost]
        //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        //{
        //    var result = await _manager
        //        .AuthenticationService
        //        .RegisterUser(userForRegistrationDto);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.TryAddModelError(error.Code, error.Description);
        //        }
        //        return BadRequest(ModelState);
        //    }

        //    return StatusCode(201);
        //}
        //bir kullanıcıya detaylı bilgilendirme eksik.
    }
}
