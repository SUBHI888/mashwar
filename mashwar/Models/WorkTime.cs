using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mashwar.Models
{
    public class WorkTime
    {
        [Key]
        public int Id { get; set; }

        // FK → Place
        [Required]
        public int PlaceId { get; set; }

        [ForeignKey("PlaceId")]
        public Place Place { get; set; }

        [Required(ErrorMessage = "Day of week is required")]
        [StringLength(20)]
        public string DayOfWeek { get; set; } // الأحد، الإثنين ... الخ

        [Required]
        public TimeSpan OpeningTime { get; set; }

        [Required]
        public TimeSpan ClosingTime { get; set; }

        public bool IsClosed { get; set; } = false; // لتحديد إذا كان النشاط مغلق في هذا اليوم
    }
}

