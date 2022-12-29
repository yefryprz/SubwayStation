namespace SubwayStation.Domain.DTOs
{
    public class FrequentlyDTO
    {
        public int FrequentlyId { get; set; }
        public SubwayDTO Subway { get; set; }
        public Guid UserId { get; set; }
    }
}
