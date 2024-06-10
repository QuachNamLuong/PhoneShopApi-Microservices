using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Product.Models
{
    public class PhoneOption
    {
        public int Id { get; set; }
        public int PhoneId { get; set; }
        public int BuiltInStorageId { get; set; }
        public int PhoneColorId { get; set; }
        [Column(TypeName = "decimal(12, 3)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Phone Phone { get; set; } = null!;
        public BuiltInStorage BuiltInStorage { get; set; } = null!;
        public PhoneColor PhoneColor { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = null!;

    }
}