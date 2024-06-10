using PhoneShopApi.Product.Dto.Phone.Color;
using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Product.Interfaces.IRepository
{
    public interface IPhoneColorRepository
    {
        Task<ICollection<PhoneColor>> GetAllPhoneColorsAsync();
        Task<PhoneColor?> GetByIdAsync(int id);
        Task<PhoneColor> CreateAsync(PhoneColor newPhoneColor);
        Task<PhoneColor?> UpdateAsync(int id, UpdatePhoneColorRequestDto updatePhoneColorRequestDto);
        Task<PhoneColor?> DeleteAsync(int id);
    }
}
