using System.ComponentModel.DataAnnotations;

namespace mashwar.Models
{
    public class RegesterModel
    {
        [Required(ErrorMessage = "Enter User Name")]
        [Display(Name = "User_Name")]
        public string User_Name { get; set; }
        [EmailAddress]
        public string User_Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Confirm_Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Confirm_Password { get; set; }
        public bool  IsBusniesOwner { get; set; }
    }
        
    
}
