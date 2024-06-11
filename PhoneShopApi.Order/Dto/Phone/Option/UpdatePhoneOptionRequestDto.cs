using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Ordering.Dto.Phone.Option
{
    public class UpdatePhoneOptionRequestDto
    {
        public int BuiltInStorageId { get; set; }
        public int PhoneColorId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
