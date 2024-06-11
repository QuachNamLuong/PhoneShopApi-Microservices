
using System.ComponentModel.DataAnnotations;

namespace PhoneShopApi.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Phone> Phones { get; set; } = [];
    }
}
