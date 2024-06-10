using PhoneShopApi.Product.Dto.Payment;
using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Product.Interfaces.IRepository
{
    public interface IPaymentRepository
    {
        Task<ICollection<Payment>> GetAllPaymentAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<Payment> CreatePaymentAsync(Payment newPayment);
        Task<Payment?> UpdatePaymentAsync(int id, UpdatePaymentRequestDto updatePaymentRequestDto);
        Task<Payment?> DeletePaymentByIdAsync(int id);
    }
}
