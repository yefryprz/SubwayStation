using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SubwayStation.Domain.Entities
{
    public class Geometric
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeometricId { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
