using PhoneShopApi.Auth.Dto.Brand;
using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Interfaces.IRepository
{
    public interface IBrandRepository
    {
        Task<ICollection<Brand>> GetAllAsync();
        Task<Brand?> GetById(int Id);
        Task<Brand> Create(Brand newBrand);
        Task<Brand?> Update(int Id, UpdateBrandRequestDto updateBrandRequestDTO);
        Task<Brand?> DeleteById(int Id);
    }
}
