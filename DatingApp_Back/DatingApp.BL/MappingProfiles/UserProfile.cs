using AutoMapper;
using DatingApp.Common.DTO.User;
using DatingApp.DAL.Entities;
using DatingApp.DAL.Extensions;

namespace DatingApp.BL.MappingProfiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt
                .MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt
                .MapFrom(src => src.DateOfBirth.CalculateAge()));
        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<RegisterDto, AppUser>();
    }
}