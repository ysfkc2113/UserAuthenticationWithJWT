using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.AcademcianService
{
    public interface IEventServiceAcademician
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllEventsAsync(LinkParameters linkParameters,
          bool trackChanges);
        Task <EventDto>GetOneEventByIdAcedemicianAsync(HttpContext httpContext,int id, bool trackChanges);
        Task<EventDto> CreateOneEventForAcademicianAsync(AcademicianEventDtoForInsertion academicianEventDtoForInsertion, HttpContext httpContext,bool trackChanges);
        Task DeleteOneEventForAcademicianAsync(int id,HttpContext httpContext,bool trackChanges);
       
        
        Task UpdateEventForAcademicianAsync(int id, AcademicianEventDtoForUpdate academicianEventDtoForUpdate , HttpContext httpContext,bool trackChanges);
        Task<(EventDtoForPatchApproved eventDtoForUpdate, Event clubEvent)> GetOneEventDtoForPatchApprovedForAcademician(HttpContext httpContext,int id,bool trackChanges);
        Task SaveChangesForPatchApprovedForAcademicianAsync( EventDtoForPatchApproved  eventDtoForPatch,Event clubEvent,HttpContext httpContext, bool trackChanges);
    }
}
