using PhoneShopApi.Auth.Dto.Brand;
using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Mappers
{
    public static class BrandMapper
    {
        public static BrandDto ToBrandDto(this Brand brandModel)
        {
            return new BrandDto
            {
                Name = brandModel.Name,
                Id = brandModel.Id
            };
        }

        public static Brand ToBrandFromCreateBrandRequestDto(this CreateBrandRequestDto upCreatebrandDto)
        {
            return new Brand
            {
                Name = upCreatebrandDto.Name
            };
        }

    }
}
