using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface IEventLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<EventDto> eventDto,
            string fields, HttpContext httpContext);
    }
}
