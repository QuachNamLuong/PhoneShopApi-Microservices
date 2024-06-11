namespace PhoneShopApi.Auth.Dto.Phone.Option
{
    public record CreateNewPhoneOptionRequest(
        int BuiltInStorageCapacity,
        string BuiltInStorageUnit,
        string PhoneColorName,
        decimal Price,
        int Quantity);

}
