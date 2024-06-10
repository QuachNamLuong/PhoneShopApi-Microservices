namespace PhoneShopApi.Product.Dto.Phone
{
    public class Item
    {
        public int PhoneId { get; set; }
        public string PhoneName { get; set; } = string.Empty;
        public int BuiltInStorageCapacity { get; set; }
        public string BuiltInStorageUnit { get; set; } = string.Empty;
        public string PhoneColorName { get; set; } = string.Empty;
        public string PhoneColorUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
