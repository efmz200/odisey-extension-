using AutoMapper;
using webapi.DTOs;
using webapi.Metadata;

namespace webapi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserData, UserRead>().ReverseMap();
            CreateMap<UserCreate, UserData>().ReverseMap();
        }
    }
}