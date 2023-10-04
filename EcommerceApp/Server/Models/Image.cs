
namespace EcommerceApp.Server.Models
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte[]? Data { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
