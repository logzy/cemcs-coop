using System.ComponentModel.DataAnnotations;

namespace COOP.Banking.Data.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "User Type is required")]
        public Enums.UserType UserType { get; set; }
        [Required(ErrorMessage = "User Type Category is required")]
        public int UserTypeCategory { get; set; }
        public string ReturnUrl { get; set; }
    }
}
