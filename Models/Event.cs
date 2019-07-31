using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public DateTime EventDate { get; set; }
        public string Theme { get; set; }
        public int QtdPeople { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Allotment> Allotments { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<SpeakerEvent> SpeakerEvents { get; set; }
    }
}
