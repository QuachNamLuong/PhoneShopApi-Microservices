using PhoneShopApi.Auth.Dto.Payment;
using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Mappers
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
