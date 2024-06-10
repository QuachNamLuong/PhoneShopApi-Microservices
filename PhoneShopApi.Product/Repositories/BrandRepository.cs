using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Product.Models;
using PhoneShopApi.Product.Dto.Brand;
using PhoneShopApi.Product.Interfaces.IRepository;
using PhoneShopApi.Product.Data;

namespace PhoneShopApi.Product.Repositories
{
    public class BrandRepository(PhoneShopDbContext context) : IBrandRepository
    {
        private readonly PhoneShopDbContext _context = context;


        public async Task<ICollection<Brand>> GetAllAsync()
        {
            var brands = await _context.Brands.ToListAsync();
            return brands;
        }

        public async Task<Brand?> GetById(int Id)
        {
            var brand = await _context.Brands.FindAsync(Id);
            return brand;
        }

        public async Task<Brand> Create(Brand newBrand)
        {
            await _context.Brands.AddAsync(newBrand);
            await _context.SaveChangesAsync();

            return newBrand;
        }


        public async Task<Brand?> Update(int Id, UpdateBrandRequestDto updateBrandRequestDTO)
        {
            var brandToUpdate = await _context.Brands.FindAsync(Id);
            if (brandToUpdate is null) return null;

            brandToUpdate.Name = updateBrandRequestDTO.Name;

            await _context.SaveChangesAsync();
            return brandToUpdate;
        }

        public async Task<Brand?> DeleteById(int Id)
        {
            var brandToDelete = await _context.Brands.FindAsync(Id);
            if (brandToDelete is null) return null;

            _context.Brands.Remove(brandToDelete);
            await _context.SaveChangesAsync();

            return brandToDelete;
        }
    }
}
