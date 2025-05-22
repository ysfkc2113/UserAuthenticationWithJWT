using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.Academician
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    //[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [ApiController]
    // [Authorize(Roles = "Academician")]
    [Route("api/academician/rolemanagment")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserRoleController : ControllerBase
    {

        private readonly IServiceManager _manager;

        public UserRoleController(IServiceManager manager)
        {
            _manager = manager;
        }

        // GET: api/admin/userroles/{userId}
        [HttpGet("username")]
        public async Task<IActionResult> GetRolesByUserId([FromQuery] string userName)
        {
            var roles = await _manager.UserRoleServiceAcademician.GetRolesByUserIdForAcademicianAsync(HttpContext,userName);
            return Ok(roles);
        }
        // POST: api/admin/userroles/assign
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToUser([FromQuery] string userName, [FromQuery] string roleName)
        {
            await _manager.UserRoleServiceAcademician.AssignRoleToUserForAcademicianAsync(HttpContext,userName, roleName);
            return NoContent();
        }
        // DELETE: api/admin/userroles/remove
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveRoleFromUser([FromQuery] string userName, [FromQuery] string roleName)
        {
            await _manager.UserRoleService.RemoveRoleFromUserAsync(userName, roleName);
            return NoContent();
        }
    }
}
