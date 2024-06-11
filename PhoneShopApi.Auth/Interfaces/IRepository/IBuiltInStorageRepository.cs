using PhoneShopApi.Auth.Dto.BuiltInStorage;
using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Interfaces.IRepository
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
