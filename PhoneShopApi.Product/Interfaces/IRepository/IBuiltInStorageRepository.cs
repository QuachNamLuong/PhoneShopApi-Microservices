using PhoneShopApi.Product.Dto.BuiltInStorage;
using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Product.Interfaces.IRepository
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
