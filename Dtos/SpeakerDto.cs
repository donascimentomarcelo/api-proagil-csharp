using System.Collections.Generic;

namespace ProAgil.WebAPI.Dtos
{
    public class SpeakerDto
    {
        public string Name { get; set; }
        public string Resume { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<SocialNetworkDto> SocialNetworks { get; set; }
        public List<EventDto> Events { get; set; }
    }
}