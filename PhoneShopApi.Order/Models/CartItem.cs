namespace PhoneShopApi.Ordering.Models
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int PhoneOptionId { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; } = null!;
        public PhoneOption PhoneOption { get; set; } = null!;
    }
}
