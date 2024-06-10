using PhoneShopApi.Product.Dto.Payment;
using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Product.Mappers
{
    public static class PaymentMapper
    {
        public static PaymentDto ToPaymentDto(this Payment paymentModel)
        {
            return new PaymentDto
            {
                Id = paymentModel.Id,
                Name = paymentModel.Name,
            };
        }

        public static Payment ToPaymentFromCreatePaymentRequestDto(
            this CreatePaymentRequestDto createPaymentRequestDto)
        {
            return new Payment
            {
                Name = createPaymentRequestDto.Name
            };
        }
    }
}
