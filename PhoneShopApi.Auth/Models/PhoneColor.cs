using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Auth.Models
{
    public class PhoneColor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<PhoneOption> PhoneOptions { get; set; } = [];
    }
}