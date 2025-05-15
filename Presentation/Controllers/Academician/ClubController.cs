using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/academician/clubs")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClubController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ClubController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public async Task<IActionResult> GetOneClubById()
        {
            var club = await _manager.ClubServiceAcademician.GetOneClubByIdForAcademicianAsync(HttpContext, true);
            return Ok(club);
        }

        //Bir clubü sadece admin(sekreter) silip ekleyebilir.
        //[HttpDelete("{id:int}")]
       
 //Bir Clubun Club Manager ı Silinirse İlişkili Yerlerden de Silmemiz Gerekiyor
        [HttpPut]
        public async Task<IActionResult> UpdateClub([FromBody] AdminClubDtoForUpdate adminClubDtoForUpdate)
        {

            var club = await _manager.ClubServiceAcademician.UpdateClubForAcademicianAsync(adminClubDtoForUpdate,HttpContext ,true);
            return Ok(club);
        }



    }
}
