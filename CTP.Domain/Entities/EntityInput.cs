using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CTP.Domain.Enums;

namespace CTP.Domain.Entities
{
    public class EntityInput
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrganizationEntityId { get; set; }
        [ForeignKey("OrganizationEntityId")]
        public virtual OrganizationEntity? Organization { get; set; }

        [Required]
        public int PreparerId { get; set; }
        [ForeignKey("PreparerId")]
        public virtual User? Preparer { get; set; }

        [Required]
        public InputType Type { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; } = string.Empty;

        public InputStatus Status { get; set; } = InputStatus.Received;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}