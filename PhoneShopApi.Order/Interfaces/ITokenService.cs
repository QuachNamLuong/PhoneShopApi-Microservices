using PhoneShopApi.Ordering.Models;

namespace PhoneShopApi.Ordering.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
