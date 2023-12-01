namespace EcommerceApp.Shared.DTOs.PageDtos
{
    public class BannerDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BannerImageURI { get; set; } = string.Empty;
        public DateTime DateModified { get; set; }
    }
}
