using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mashwar.Models
{

    
        public class Place
        {
        [Key]
            public int PlaceID { get; set; }

            [Required (ErrorMessage ="Please enter this faild"), StringLength(200)]
            public string Name { get; set; }
        [Required(ErrorMessage ="Please enter Your description")]
            public string Description { get; set; }
        [Required(ErrorMessage ="Please enter your location")]
            public string Location { get; set; }
        [Required(ErrorMessage ="Please enter your Rating")]
            public double Rating { get; set; } = 0;
        [Required (ErrorMessage ="please Enter Your Price Level")]
            public string PriceLevel { get; set; }

        public string? Place_Image { get; set; }
        [Required]
        public int? User_Id { get; set; }
        [ForeignKey("User_Id")]
        public AppUser Users { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<WorkTime> WorkTimes { get; set; }

    }
}

