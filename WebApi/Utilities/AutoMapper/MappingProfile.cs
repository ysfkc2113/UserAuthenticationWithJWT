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
            //For Admin
            CreateMap<AdminEventDtoForUpdate, Event>()
                     .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Event,EventDtoForPatchApproved>();
            CreateMap<Club, ClubDto>().ReverseMap();
            CreateMap<ClubDtoForInsertion, Club>();
            CreateMap<AdminClubDtoForUpdate, Club>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
