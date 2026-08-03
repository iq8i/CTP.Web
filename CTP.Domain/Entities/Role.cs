using System.ComponentModel.DataAnnotations;

namespace CTP.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleCode { get; set; } = string.Empty; // مثل: LEADER, AMBASSADOR

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // علاقة المستخدمين بالأدوار (Many-to-Many)
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}