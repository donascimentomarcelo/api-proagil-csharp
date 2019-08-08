using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class EventDto
    {
        [Required(ErrorMessage="The Theme Place must be filled out!")]
        [StringLength(100, MinimumLength=3, ErrorMessage="The Place field must have between 3 and 100 characters!")]
        public string Place { get; set; }
        public string EventDate { get; set; }

        [Required(ErrorMessage="The Theme field must be filled out!")]
        public string Theme { get; set; }

        [Range(2, 20000, ErrorMessage="The QtdPeople field must be a quantity between 2 and 20000!")]
        public int QtdPeople { get; set; }

        [Phone]
        public string Phone { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public List<AllotmentDto> Allotments { get; set; }
        public List<SocialNetworkDto> SocialNetworks { get; set; }
        public List<SpeakerDto> Speakers { get; set; }
    }
}