using PhoneShopApi.Ordering.Dto.Payment;
using PhoneShopApi.Ordering.Models;

namespace PhoneShopApi.Ordering.Interfaces.IRepository
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
