using System.ComponentModel.DataAnnotations;

namespace mashwar.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        public int NumberOfPeople { get; set; }

        // FK → User
        public int UserId { get; set; }
        public AppUser User { get; set; }

        // FK → Place
        public int PlaceId { get; set; }
        public Place Place { get; set; }

    }
}
