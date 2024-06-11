using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Auth.Models
{
    public class BuiltInStorage
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Unit { get; set; } = string.Empty;

        public ICollection<PhoneOption> PhoneOptions { get; set; } = [];
    }
}