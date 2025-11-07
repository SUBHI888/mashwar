using System.ComponentModel.DataAnnotations;

namespace mashwar.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // 1-5 stars

        public string Comment { get; set; }

        // FK → User
        public int UserId { get; set; }
        public AppUser User { get; set; }

        // FK → Place
        public int PlaceId { get; set; }
        public Place Place { get; set; }

    }
}
