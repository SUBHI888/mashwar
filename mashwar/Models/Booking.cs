using System.ComponentModel.DataAnnotations;

namespace mashwar.Models
{
    public enum BookingStatus
    {
        Confirmed = 1,
        Cancelled = 2
    }

    public enum TableLocation
    {
        Indoor = 1,
        Outdoor = 2,
        VIP = 3
    }

    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public TimeSpan BookingTime { get; set; }

        [Required]
        [Range(1, 50)]
        public int NumberOfPeople { get; set; }

        [Required]
        public TableLocation TableLocation { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }





}

