using System;

namespace ProAgil.WebAPI.Models
{
    public class Allotment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Quantity { get; set; }
        public int EventId { get; set; }   
        public Event Event { get; set; }
    }
}