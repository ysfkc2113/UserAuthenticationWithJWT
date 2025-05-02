using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventDtoForUpdate, Event>().ReverseMap();
            CreateMap<Event, EventDto>();
            CreateMap<EventDtoForInsertion, Event>();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
