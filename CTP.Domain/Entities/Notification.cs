using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTP.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        [StringLength(255)]
        public string? ActionUrl { get; set; } // الرابط الذي سيتم توجيه المستخدم إليه عند النقر

        [StringLength(50)]
        public string IconClass { get; set; } = "bi-bell-fill";

        [StringLength(50)]
        public string TextColorClass { get; set; } = "text-dark";

        public bool IsRead { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}