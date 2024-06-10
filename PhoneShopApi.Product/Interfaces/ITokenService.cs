using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Product.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
