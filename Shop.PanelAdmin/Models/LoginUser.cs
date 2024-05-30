using System.ComponentModel.DataAnnotations;

namespace Shop.PanelAdmin.Models
{
    public class LoginUser
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = default!;

        [DataType(DataType.Password)]   
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; } = default!;
    }
}
