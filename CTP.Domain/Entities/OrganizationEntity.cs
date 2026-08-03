using System.ComponentModel.DataAnnotations;

namespace CTP.Domain.Entities
{
    public class OrganizationEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string EntityCode { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string EntityName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}