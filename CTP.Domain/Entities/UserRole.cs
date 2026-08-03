using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTP.Domain.Entities
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.Now;

        public DateTime? ExpiresDate { get; set; }

        public bool IsActive { get; set; } = true;

        // العلاقات
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; } = null!;
    }
}