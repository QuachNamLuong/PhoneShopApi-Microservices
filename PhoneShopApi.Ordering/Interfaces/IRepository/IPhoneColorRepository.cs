using PhoneShopApi.Ordering.Dto.Phone.Color;
using PhoneShopApi.Ordering.Models;

namespace PhoneShopApi.Ordering.Interfaces.IRepository
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
