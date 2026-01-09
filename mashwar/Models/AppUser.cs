using System.ComponentModel.DataAnnotations;

namespace mashwar.Models
{
    public class AppUser
    {
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username must be at most 50 characters")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string User_Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string User_Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime Create_Date { get; set; } = DateTime.Now;

        public ICollection<Place> Places { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<BusinessLicense> BusinessLicenses { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Category> categories { get; set; }
       


    }

}
