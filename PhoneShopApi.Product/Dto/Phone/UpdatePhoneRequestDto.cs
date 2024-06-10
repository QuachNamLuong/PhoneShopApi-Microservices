namespace PhoneShopApi.Dto.Phone
{
    public class UpdatePhoneRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsSelling { get; set; }
        public int BrandId { get; set; }
    }
}
