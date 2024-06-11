namespace PhoneShopApi.Ordering.Dto.Phone.Color
{
    public class PhoneColorDto : CreatePhoneColorRequestDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
