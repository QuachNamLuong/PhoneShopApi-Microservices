namespace PhoneShopApi.Auth.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = [];
    }
}
