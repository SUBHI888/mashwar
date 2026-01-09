using System.ComponentModel.DataAnnotations;

namespace mashwar.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
