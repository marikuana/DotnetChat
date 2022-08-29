using AutoMapper;
using DotnetChat.Data.Models;
using DotnetChat.Models;

namespace DotnetChat.Mapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(x => x.ChatId, q => q.MapFrom(w => w.ChatId));

            CreateMap<Message, EditMessageViewModel>()
                .ForMember(x => x.Text, q => q.MapFrom(w => w.Text));

            CreateMap<Message, DeleteMessageViewModel>();
        }
    }
}
