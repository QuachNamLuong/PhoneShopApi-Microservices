

namespace PhoneShopApi.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}
