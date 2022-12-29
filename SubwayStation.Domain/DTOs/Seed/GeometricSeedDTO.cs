namespace SubwayStation.Domain.DTOs.Seed
{
    public class GeometricSeedDTO
    {
        public GeometricSeedDTO()
        {
            Coordinates = new List<decimal>();
        }

        public string Type { get; set; }
        public List<decimal> Coordinates { get; set; }
    }
}
