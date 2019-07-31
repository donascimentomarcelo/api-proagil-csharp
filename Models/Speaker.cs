using System.Collections.Generic;

namespace ProAgil.WebAPI.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<SpeakerEvent> SpeakerEvents { get; set; }
    }
}