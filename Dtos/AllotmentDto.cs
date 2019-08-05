namespace ProAgil.WebAPI.Dtos
{
    public class AllotmentDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Quantity { get; set; }
    }
}