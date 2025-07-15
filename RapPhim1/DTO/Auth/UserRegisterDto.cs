using System.ComponentModel.DataAnnotations;

namespace RapPhim1.DTO.Auth
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
