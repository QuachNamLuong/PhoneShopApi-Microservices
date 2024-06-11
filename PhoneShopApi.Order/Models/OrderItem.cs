using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int PhoneOptionId { get; set; }
        [Column(TypeName = "decimal(12,3)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; } = null!;
        public PhoneOption PhoneOption { get; set; } = null!;
    }
}
