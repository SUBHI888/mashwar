using System.ComponentModel.DataAnnotations;

namespace mashwar.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public string User_Email { get; set; }
        public string User_Password { get; set; }
    }
}
