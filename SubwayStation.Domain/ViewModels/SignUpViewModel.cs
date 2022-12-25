using System.ComponentModel.DataAnnotations;

namespace SubwayStation.Domain.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }        
        public string? Phone { get; set; }
    }
}
