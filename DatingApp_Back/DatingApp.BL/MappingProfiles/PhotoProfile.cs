using AutoMapper;
using DatingApp.Common.DTO.Photo;
using DatingApp.DAL.Entities;

namespace DatingApp.BL.MappingProfiles;

public class PhotoProfile: Profile
{
    public PhotoProfile()
    {
        CreateMap<Photo, PhotoDto>();
    }
}