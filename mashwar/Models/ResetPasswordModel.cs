using System.ComponentModel.DataAnnotations;

namespace mashwar.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "كلمة السر لازم تكون 6 أحرف على الأقل")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "كلمة السر وتأكيدها غير متطابقين")]
        public string ConfirmPassword { get; set; }
    }
}
