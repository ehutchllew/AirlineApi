namespace AirlineApi.Models
{
    public class FlightDetailReadDto
    {
        public int Id { get; set; }

        public string FlightCode { get; set; }

        public decimal Price { get; set; }
    }
}