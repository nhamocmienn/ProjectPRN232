using System.ComponentModel.DataAnnotations;

namespace RapPhim1.DTO.Common
{
    public class UserUpdateProfileDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
