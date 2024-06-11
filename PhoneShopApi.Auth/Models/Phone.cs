using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace PhoneShopApi.Auth.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public bool IsSelling { get; set; } = false;

        public Brand Brand { get; set; } = null!;
        public ICollection<PhoneOption> PhoneOptions { get; set; } = [];
        public PhoneDetail PhoneDetail { get; set; } = null!;
    }
}