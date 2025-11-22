using System.ComponentModel.DataAnnotations;

namespace MyMarket.Models
{

    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? FullName { get; set; } // 👈 N

        [Required, Phone]
        public string? PhoneNumber { get; set; } // 👈 Numéro de téléphone

        [Required, EmailAddress]
        public string? Email { get; set; } // 👈 Email

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; } // 👈 Mot de passe

        [StringLength(20)]
        public string Role { get; set; } = "User"; // 👈 Par défaut
    }
}
    