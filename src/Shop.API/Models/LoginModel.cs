using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
