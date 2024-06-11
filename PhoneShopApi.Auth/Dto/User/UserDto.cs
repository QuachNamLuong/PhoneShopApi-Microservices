using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Dto.User
{
    public class UserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
