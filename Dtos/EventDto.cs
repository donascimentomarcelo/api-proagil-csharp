using System.Collections.Generic;

namespace ProAgil.WebAPI.Dtos
{
    public class EventDto
    {
        public string Place { get; set; }
        public string EventDate { get; set; }
        public string Theme { get; set; }
        public int QtdPeople { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<AllotmentDto> Allotments { get; set; }
        public List<SocialNetworkDto> SocialNetworks { get; set; }
        public List<SpeakerDto> Speakers { get; set; }
    }
}