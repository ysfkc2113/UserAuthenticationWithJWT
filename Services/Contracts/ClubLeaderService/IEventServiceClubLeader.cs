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

namespace Services.Contracts.ClubLeaderService
{
    public interface IEventServiceClubLeader
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllEventsForClubManagerAsync(LinkParameters linkParameters,bool trackChanges);
        Task<EventDto> CreateOneEventForClubLeaderAsync(AcademicianEventDtoForInsertion academicianEventDtoForInsertion, HttpContext httpContext, bool trackChanges);
        Task DeleteOneEventForClubManagerAsync(int id, HttpContext httpContext, bool trackChanges);


        Task UpdateEventForClubManagerAsync(int id, AcademicianEventDtoForUpdate academicianEventDtoForUpdate, HttpContext httpContext, bool trackChanges);
    }
}
