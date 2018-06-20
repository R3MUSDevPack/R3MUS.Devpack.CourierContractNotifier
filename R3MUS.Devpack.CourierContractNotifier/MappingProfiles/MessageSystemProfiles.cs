using AutoMapper;
using R3MUS.Devpack.CourierContractNotifier.Models;
using R3MUS.Devpack.Discord.Models.Embeds;
using R3MUS.Devpack.Slack;
using System.Collections.Generic;

namespace R3MUS.Devpack.CourierContractNotifier.MappingProfiles
{
    public class MessageSystemProfiles : Profile
    {
        public MessageSystemProfiles()
        {
            CreateMap<NotificationRequest, EmbedPostModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => "Bishop"))
                .ForMember(dest => dest.Embeds, opt => opt.MapFrom(src => new List<Embed>()));

            CreateMap<NotificationRequest, Embed>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.GetTitle()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.GetMessageText()))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<NotificationRequest, MessagePayload>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => "Bishop"))
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => new List<MessagePayloadAttachment>()))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<NotificationRequest, MessagePayloadAttachment>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.GetTitle()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.GetMessageText()))
                .ForMember(dest => dest.Colour, opt => opt.MapFrom(src => src.GetColour()))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
