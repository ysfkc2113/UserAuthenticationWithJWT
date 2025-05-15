using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Entities.DataTransferObjects
{
    public record LinkParameters
    {
        public EventParameters? EventParameters { get; init; }
        public AcademicianEventParameters? AcademicianEventParameters { get; init; }
        //public ClubParameters? ClubParameters { get; init; }
        public HttpContext HttpContext { get; init; }
    }
}
