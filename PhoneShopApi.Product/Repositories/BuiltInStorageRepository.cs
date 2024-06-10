using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Data;
using PhoneShopApi.Dto.BuiltInStorage;
using PhoneShopApi.Interfaces.IRepository;
using PhoneShopApi.Models;

namespace PhoneShopApi.Repositories
{
    public class BuiltInStorageRepository(PhoneShopDbContext context) : IBuiltInStorageRepository
    {
        private readonly PhoneShopDbContext _context = context;

        public async Task<ICollection<BuiltInStorage>> GetAllRamsAsync()
        {
            var builtInStorages = await _context.BuiltInStorages.ToListAsync();

            return builtInStorages;
        }

        public async Task<BuiltInStorage?> GetByIdAsync(int id)
        {
            var builtInStorage = await _context.BuiltInStorages.FindAsync(id);

            return builtInStorage;
        }

        public async Task<BuiltInStorage> CreateAsync(BuiltInStorage newBuiltInStorage)
        {
            await _context.BuiltInStorages.AddAsync(newBuiltInStorage);
            await _context.SaveChangesAsync();

            return newBuiltInStorage;
        }


        public async Task<BuiltInStorage?> UpdateAsync(int id, UpdateBuiltInStorageRequestDto updateBuiltInStorageRequestDto)
        {
            var builtInStorage = await _context.BuiltInStorages.FindAsync(id);
            if (builtInStorage is null) return null;

            builtInStorage.Capacity = updateBuiltInStorageRequestDto.Capacity;
            builtInStorage.Unit = updateBuiltInStorageRequestDto.Unit;

            await _context.SaveChangesAsync();
            return builtInStorage;
        }

        public async Task<BuiltInStorage?> DeleteAsync(int id)
        {
            var builtInStorageToDelete = await _context.BuiltInStorages.FindAsync(id);
            if (builtInStorageToDelete is null) return null;

            _context.BuiltInStorages.Remove(builtInStorageToDelete);
            await _context.SaveChangesAsync();

            return builtInStorageToDelete;
        }
    }
}
