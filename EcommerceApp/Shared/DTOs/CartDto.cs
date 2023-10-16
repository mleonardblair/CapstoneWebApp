
namespace EcommerceApp.Shared.DTOs
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
