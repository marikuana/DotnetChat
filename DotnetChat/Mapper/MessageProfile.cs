using AutoMapper;
using DotnetChat.Data.Models;
using DotnetChat.Models;

namespace DotnetChat.Mapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageViewModel>();

            CreateMap<Message, EditMessageViewModel>()
                .ForMember(x => x.Text, q => q.MapFrom(w => w.Text));
        }
    }
}
