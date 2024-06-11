using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
