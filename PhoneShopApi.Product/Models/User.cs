

using Microsoft.AspNetCore.Identity;

namespace PhoneShopApi.Product.Models
{
    public class User : IdentityUser
    {
        public string Address { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public Cart Cart { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<IdentityRole> Roles { get; set; } = [];
    }
}
