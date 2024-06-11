namespace PhoneShopApi.Dto.BuiltInStorage
{
    public class CreateBuiltInStorageRequestDto
    {
        public int Capacity { get; set; }
        public string Unit { get; set; } = null!;
    }
}
