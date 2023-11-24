
namespace EcommerceApp.Shared.DTOs
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public List<CartProductResponse> CartProducts { get; set; } = new List<CartProductResponse>();
    }
}
