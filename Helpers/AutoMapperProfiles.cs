using AutoMapper;
using ProAgil.WebAPI.Dtos;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Event, EventDto>();
            CreateMap<Speaker, SpeakerDto>();
            CreateMap<Allotment, AllotmentDto>();
            CreateMap<SocialNetwork, SocialNetworkDto>();
        }
    }
}