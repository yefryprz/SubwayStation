using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubwayStation.Domain.Entities
{
    public class Subways
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ObjectId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Line { get; set; }
        public int GeometricId { get; set; }

        [ForeignKey(nameof(GeometricId))]
        public Geometric Geometric { get; set; }
    }
}
