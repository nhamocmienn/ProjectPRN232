namespace RapPhim1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "User"; // "User" hoặc "Admin"
    }
}
