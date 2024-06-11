using PhoneShopApi.Dto.Brand;
using PhoneShopApi.Models;

namespace PhoneShopApi.Interfaces.IRepository
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
