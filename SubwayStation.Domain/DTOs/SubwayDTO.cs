namespace SubwayStation.Domain.DTOs
{
    public class SubwayDTO
    {
        public int ObjectId { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
        public string? Line { get; set; }
        public GeometricDTO Geometric { get; set; }
    }
}
