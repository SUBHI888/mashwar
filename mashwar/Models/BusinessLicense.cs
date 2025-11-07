using mashwar.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mashwar.Models
{
    public class BusinessLicense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LicenseFilePath { get; set; }

        [Required]
        public string RegistryFilePath { get; set; }

        [Required]
        public string ApprovalStatus { get; set; } = "Pending"; // Pending / Approved / Rejected

        // FK → AppUser
        public int User_Id { get; set; }
        [ForeignKey("User_Id")]
        public AppUser User { get; set; }
    }
}
