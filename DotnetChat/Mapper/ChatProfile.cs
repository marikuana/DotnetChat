using AutoMapper;
using DotnetChat.Data.Models;
using DotnetChat.Models;

namespace DotnetChat.Mapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Chat, ChatViewModel>()
                .ForMember(x => x.Messages, q => q.MapFrom(w => w.Messages))
                .ForMember(x => x.Users, q => q.MapFrom(w => w.Members));
        }
    }
}
