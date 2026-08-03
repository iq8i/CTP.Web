using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTP.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? JobTitle { get; set; }

        // تم استبدال AdminSectionId بـ OrganizationEntityId ليتناسب مع المنصة الجديدة
        public int? OrganizationEntityId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? LastLogin { get; set; }

        // ميزات الأمان المستوردة من مشروعك القديم
        public bool MustChangePassword { get; set; } = false;
        public DateTime? PasswordExpiresDate { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; }
        [StringLength(500)]
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresDate { get; set; }

        // العلاقات
        [ForeignKey("OrganizationEntityId")]
        public virtual OrganizationEntity? Organization { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}