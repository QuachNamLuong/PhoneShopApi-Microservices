namespace PhoneShopApi.Product.Dto.Phone
{
    public class PhoneItemDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public ICollection<Item> Phones { get; set; } = null!;
    }
}
