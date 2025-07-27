using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models
{
    public class User
    {
        public int id { get; set; }

        [Required, MaxLength(15, ErrorMessage ="Username cannot exceed 15 characters")]
        [MinLength(5, ErrorMessage ="Username should have atleast 5 characters")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; } = string.Empty;


    }
}
