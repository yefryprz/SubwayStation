namespace SubwayStation.Domain.DTOs.Seed
{
    public class SubwaysSeedDTO
    {
        public string? Url { get; set; }
        public string? Name { get; set; }
        public string? Line { get; set; }
        public int GeometricId { get; set; }
        public GeometricSeedDTO? The_geom { get; set; }
    }
}
