using AutoMapper;
using DatingApp.Common.DTO.Message;
using DatingApp.DAL.Entities;

namespace DatingApp.BL.MappingProfiles;

public class MessageProfile: Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(d => d.SenderPhotoUrl, o =>
                o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain)!.Url))
            .ForMember(d => d.RecipientPhotoUrl, o =>
                o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain)!.Url));
    }
}