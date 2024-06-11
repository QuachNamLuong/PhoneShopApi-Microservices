using PhoneShopApi.Models;

namespace PhoneShopApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
