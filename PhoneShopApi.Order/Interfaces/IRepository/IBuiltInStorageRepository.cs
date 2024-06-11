using PhoneShopApi.Ordering.Dto.BuiltInStorage;
using PhoneShopApi.Ordering.Models;

namespace PhoneShopApi.Ordering.Interfaces.IRepository
{
    public interface IBuiltInStorageRepository
    {
        Task<ICollection<BuiltInStorage>> GetAllRamsAsync();
        Task<BuiltInStorage?> GetByIdAsync(int id);
        Task<BuiltInStorage> CreateAsync(BuiltInStorage newBuiltInStorage);
        Task<BuiltInStorage?> UpdateAsync(int id, UpdateBuiltInStorageRequestDto updateBuiltInStorageRequestDto);
        Task<BuiltInStorage?> DeleteAsync(int id);
    }
}
