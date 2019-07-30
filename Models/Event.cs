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
        public string EventDate { get; set; }
        public string Theme { get; set; }
        public int QtdPeople { get; set; }
        public string Allotment { get; set; }
    }
}
