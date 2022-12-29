using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubwayStation.Domain.Entities
{
    public class Frequently : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FrequentlyId { get; set; }
        public int ObjectId { get; set; }

        [ForeignKey(nameof(ObjectId))]
        public Subways Subways { get; set; }
    }
}
