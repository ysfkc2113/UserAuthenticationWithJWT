using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

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
            CreateMap<Event, EventDtoForPatchApproved>();
            CreateMap<Club, ClubDto>().ReverseMap();
            CreateMap<ClubDtoForInsertion, Club>();
            CreateMap<AdminClubDtoForUpdate, Club>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Club_User, AdminClubUserDtoRelations>();
            CreateMap<User, AdminUsersDto>();
            //academician
            CreateMap<AcademicianEventParameters, EventParameters>();
            CreateMap<AcademicianEventDtoForInsertion, Event>();
            CreateMap<AcademicianEventDtoForUpdate, Event>()
                       .ForAllMembers(opts =>
          opts.Condition((src, dest, srcMember, destMember, context) =>
          {
              if (srcMember == null) return false;
              if (srcMember is DateTime dateTimeValue)
                  return dateTimeValue != default(DateTime); // 0001-01-01T00:00:00
              return true;
          }));

            CreateMap<EventDtoForPatchApproved, Event>()
                     .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
