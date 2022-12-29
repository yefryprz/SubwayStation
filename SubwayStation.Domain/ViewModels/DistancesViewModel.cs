using System.ComponentModel.DataAnnotations;

namespace SubwayStation.Domain.ViewModels
{
    public class DistancesViewModel
    {
        [Required]
        public int ObjectIdFrom { get; set; }
        [Required]
        public double ObjectIdTo { get; set; }
    }
}
