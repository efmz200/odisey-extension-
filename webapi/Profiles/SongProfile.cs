
using AutoMapper;
using webapi.DTOs;
using webapi.Metadata;

namespace webapi.Profiles
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<SongData, SongRead>().ReverseMap();
            CreateMap<SongCreate, SongData>().ReverseMap();
        
        }
    }
}