using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mashwar.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        [StringLength(100)]
        public string MessageType { get; set; }

        [Required]
        [StringLength(300)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;

        // FK → AppUser
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
