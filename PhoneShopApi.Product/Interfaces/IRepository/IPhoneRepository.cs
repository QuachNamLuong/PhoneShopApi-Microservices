using PhoneShopApi.Product.Dto.Phone;
using PhoneShopApi.Product.Helper;
using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Product.Interfaces.IRepository
{
    public interface IPhoneRepository
    {
        Task<ICollection<Phone>> GetAllPhonesSellingAsync();
        Task<ICollection<PhoneItemDto>> GetAllPhonesSellingFollowBrandAsync(QueryPhone query);
        Task<ICollection<PhoneItemDto>> AdminGetAllPhonesSellingFollowBrandAsync(QueryPhone query);
        Task<ICollection<Phone>> GetAllAsync();
        Task<Phone?> GetByIdAsync(int id);
        Task<Phone> CreateAsync(Phone newPhone);
        Task<Phone?> UpdateAsync(int id, UpdatePhoneRequestDto updatePhoneRequestDto);
        Task<Phone?> DeleteByIdAsync(int id);
    }
}
