using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Shared.DTOs
{
    public class TagDto
    {
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        public List<Guid>? ProductTagIds { get; set; }
    }
}
