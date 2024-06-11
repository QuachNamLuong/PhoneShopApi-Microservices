using PhoneShopApi.Ordering.Dto.Phone;
using PhoneShopApi.Ordering.Helper;
using PhoneShopApi.Ordering.Models;

namespace PhoneShopApi.Ordering.Interfaces.IRepository
{
    public interface IPhoneRepository
    {
        Task<ICollection<Phone>> GetAllPhonesSellingAsync();
        Task<ICollection<PhoneItemDto>> GetAllPhonesSellingFollowBrandAsync(QueryPhone query);
        Task<ICollection<Phone>> GetAllAsync();
        Task<Phone?> GetByIdAsync(int id);
        Task<Phone> CreateAsync(Phone newPhone);
        Task<Phone?> UpdateAsync(int id, UpdatePhoneRequestDto updatePhoneRequestDto);
        Task<Phone?> DeleteByIdAsync(int id);
    }
}
