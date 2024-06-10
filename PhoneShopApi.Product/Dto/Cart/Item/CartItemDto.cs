namespace PhoneShopApi.Dto.Cart.Item
{
    public class CartItemDto
    {
        public int PhoneOptionId { get; set; }
        public string PhoneName {get; set;} = string.Empty;
        public string PhoneColor {get; set;} = string.Empty;
        public string Stogare {get; set;} = string.Empty;
        public string ImageUrl {get; set;} = string.Empty;
        public decimal Price {get; set;} 
        public int Quantity { get; set; }
    }
}
