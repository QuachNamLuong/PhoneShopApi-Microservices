using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Ordering.Mappers;
using PhoneShopApi.Ordering.Data;
using PhoneShopApi.Ordering.Dto.Phone.Color;
using PhoneShopApi.Ordering.Interfaces.IRepository;
using PhoneShopApi.Ordering.Models;

namespace PhoneShopApi.Ordering.Repositories
{
    public class PhoneColorRepository(PhoneShopDbContext context) : IPhoneColorRepository
    {
        private readonly PhoneShopDbContext _context = context;

        public async Task<ICollection<PhoneColor>> GetAllPhoneColorsAsync()
        {
            var phoneColors = await _context.PhoneColors.ToListAsync();
            return phoneColors;
        }


        public async Task<PhoneColor?> GetByIdAsync(int id)
        {
            var phoneColor = await _context.PhoneColors.FindAsync(id);
            return phoneColor;
        }

        public async Task<PhoneColor> CreateAsync(PhoneColor newPhoneColor)
        {
            await _context.PhoneColors.AddAsync(newPhoneColor);
            await _context.SaveChangesAsync();

            return newPhoneColor;
        }

        public async Task<PhoneColor?> UpdateAsync(int id, UpdatePhoneColorRequestDto updatePhoneColorRequestDTO)
        {
            var phoneColorToUpdate = await _context.PhoneColors.FindAsync(id);
            if (phoneColorToUpdate is null) return null;

            phoneColorToUpdate.Name = updatePhoneColorRequestDTO.Name;

            await _context.SaveChangesAsync();
            return phoneColorToUpdate;
        }

        public async Task<PhoneColor?> DeleteAsync(int id)
        {
            var phoneColorToDelete = await _context.PhoneColors.FindAsync(id);
            if (phoneColorToDelete is null) return null;

            _context.PhoneColors.Remove(phoneColorToDelete);
            await _context.SaveChangesAsync();

            return phoneColorToDelete;
        }
    }
}
