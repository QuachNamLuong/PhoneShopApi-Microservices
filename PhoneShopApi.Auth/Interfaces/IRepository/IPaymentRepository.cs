using PhoneShopApi.Auth.Dto.Payment;
using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Interfaces.IRepository
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
