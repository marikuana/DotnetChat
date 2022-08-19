using AutoMapper;
using DotnetChat.Data.Models;
using DotnetChat.Models;

namespace DotnetChat.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.UserName, q => q.MapFrom(w => w.Login));
        }
    }
}
