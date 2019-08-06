using System.Linq;
using AutoMapper;
using ProAgil.WebAPI.Dtos;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Event, EventDto>()
                .ForMember(recipient => recipient.Speakers, opt => {
                    opt.MapFrom(src => src.SpeakerEvents.Select(x => x.Speaker).ToList());
                });
            CreateMap<Speaker, SpeakerDto>()
                .ForMember(recipient => recipient.Events, opt => {
                    opt.MapFrom(src => src.SpeakerEvents.Select(x => x.Event).ToList());
                });
            CreateMap<Allotment, AllotmentDto>();
            CreateMap<SocialNetwork, SocialNetworkDto>();
        }
    }
}