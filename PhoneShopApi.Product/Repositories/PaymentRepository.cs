using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Product.Data;
using PhoneShopApi.Product.Dto.Payment;
using PhoneShopApi.Product.Interfaces.IRepository;
using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Product.Repositories
{
    public class PaymentRepository(PhoneShopDbContext context) : IPaymentRepository
    {
        private PhoneShopDbContext _context = context;

        public async Task<ICollection<Payment>> GetAllPaymentAsync()
        {
            var payments = await _context.Payments.ToListAsync();
            return payments;
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            return payment;
        }

        public async Task<Payment> CreatePaymentAsync(Payment newPayment)
        {
            await _context.Payments.AddAsync(newPayment);
            await _context.SaveChangesAsync();

            return newPayment;
        }


        public async Task<Payment?> UpdatePaymentAsync(int id, UpdatePaymentRequestDto updatePaymentRequestDto)
        {
            var paymentToUpdate = await _context.Payments.FindAsync(id);
            if (paymentToUpdate is null) return null;

            paymentToUpdate.Name = updatePaymentRequestDto.Name;
            await _context.SaveChangesAsync();

            return paymentToUpdate;
        }

        public async Task<Payment?> DeletePaymentByIdAsync(int id)
        {
            var paymentToDelete = await _context.Payments.FindAsync(id);
            if (paymentToDelete is null) return null;

            _context.Payments.Remove(paymentToDelete);
            await _context.SaveChangesAsync();

            return paymentToDelete;
        }
    }
}
