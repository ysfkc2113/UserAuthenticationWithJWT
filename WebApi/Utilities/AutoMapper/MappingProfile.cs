using AutoMapper;
using AutoMapper.Execution;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System.Net.Mail;

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
                 .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember, destMember, context) =>
                    {
                        if (srcMember == null) return false;
                        if (srcMember is DateTime dateTimeValue)
                            return dateTimeValue != default(DateTime); // 0001-01-01T00:00:00
                        return true;
                    })); 
            CreateMap<Event, EventDtoForPatchApproved>();
            CreateMap<Club, ClubDto>().ReverseMap();
            CreateMap<ClubDtoForInsertion, Club>();
            CreateMap<AdminClubDtoForUpdate, Club>()
                 .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember, destMember, context) =>
                    {
                        if (srcMember == null) return false;
                        if (srcMember is DateTime dateTimeValue)
                            return dateTimeValue != default(DateTime); // 0001-01-01T00:00:00
                        return true;
                    })); 
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
                 .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember, destMember, context) =>
                    {
                        if (srcMember == null) return false;
                        if (srcMember is DateTime dateTimeValue)
                            return dateTimeValue != default(DateTime); // 0001-01-01T00:00:00
                        return true; 
                    }));
                  
            CreateMap<Club_User,AdminUsersDto>();
            //ClubLeader
            CreateMap<ClubManagerDtoForUpdate, Club>()
                .ForAllMembers(opts =>
                   opts.Condition((src, dest, srcMember, destMember, context) =>
                   {
                       if (srcMember == null) return false;
                       if (srcMember is DateTime dateTimeValue)
                           return dateTimeValue != default(DateTime); // 0001-01-01T00:00:00
                       return true;
                   }));
            //Users
            CreateMap<UserDtoForMember,User>()
                .ReverseMap()
                 .ForAllMembers(opts =>
                   opts.Condition((src, dest, srcMember, destMember, context) =>
                   {
                       if (srcMember == null) return false;
                       if (srcMember is DateTime dateTimeValue)
                           return dateTimeValue != default(DateTime); // 0001-01-01T00:00:00
                       return true;
                   }));
            CreateMap<MemberClubUserDtoRelations, Club_User>()
                .ReverseMap()
                 .ForAllMembers(opts =>
                   opts.Condition((src, dest, srcMember, destMember, context) =>
                   {
                       if (srcMember == null) return false;
                       if (srcMember is DateTime dateTimeValue)
                           return dateTimeValue != default(DateTime); // 0001-01-01T00:00:00
                       return true;
                   }));

        }
    }
}


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 1. User -> UserDtoForMember dönüşümü için özel kurallar
        CreateMap<User, UserDtoForMember>()
            // Email (string'den MailAddress'e) dönüşümü
            .ForMember(dest => dest.Email,
                       opt => opt.ConvertUsing(new StringToMailAddressConverter(), src => src.Email))
            // Tüm üyeler için genel koşulları uygula (burada DateTime default değeri kontrolü)
            .ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember, destMember, context) =>
                {
                    // Eğer kaynak üye null ise map'leme
                    if (srcMember == null) return false;

                    // Eğer kaynak üye DateTime ise ve default değeri (0001-01-01T00:00:00) ise map'leme
                    if (srcMember is DateTime dateTimeValue)
                        return dateTimeValue != default(DateTime);

                    // Diğer durumlar için map'lemeye devam et
                    return true;
                });
            });

        // 2. UserDtoForMember -> User dönüşümü için özel kurallar (ReverseMap() ile)
        // ReverseMap() çağrısı, yukarıdaki CreateMap'ten hemen sonra gelmelidir.
        // ReverseMap() ile oluşturulan ters mapping'e de ayrı ForMember kuralı uygulanabilir.
        CreateMap<UserDtoForMember, User>()
            // Email (MailAddress'ten string'e) dönüşümü
            .ForMember(dest => dest.Email,
                       opt => opt.ConvertUsing(new MailAddressToStringConverter(), src => src.Email))
            // Tüm üyeler için genel koşulları uygula (aynı DateTime default değeri kontrolü)
            .ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember, destMember, context) =>
                {
                    if (srcMember == null) return false;
                    if (srcMember is DateTime dateTimeValue)
                        return dateTimeValue != default(DateTime);
                    return true;
                });
            });



    }

    // Custom Converter: string -> MailAddress
    public class StringToMailAddressConverter : IValueConverter<string, MailAddress>
    {
        public MailAddress Convert(string sourceMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(sourceMember))
                return null;
            try
            {
                return new MailAddress(sourceMember);
            }
            catch (FormatException)
            {
                // E-posta formatı geçersizse null dön.
                // Bu durumu loglamak isteyebilirsiniz.
                return null;
            }
        }
    }

    // Custom Converter: MailAddress -> string
    public class MailAddressToStringConverter : IValueConverter<MailAddress, string>
    {
        public string Convert(MailAddress sourceMember, ResolutionContext context)
        {
            return sourceMember?.Address; // MailAddress null ise veya Address propertisi null ise null döner
        }
    }
}
