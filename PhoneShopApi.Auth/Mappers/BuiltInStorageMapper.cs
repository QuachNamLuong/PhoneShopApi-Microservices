using PhoneShopApi.Auth.Dto.BuiltInStorage;
using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Mappers
{
    public static class BuiltInStorageMapper
    {
        public static BuiltInStorageDto ToRamDto(this BuiltInStorage ram)
        {
            return new BuiltInStorageDto
            {
                Id = ram.Id,
                Capacity = ram.Capacity,
                Unit = ram.Unit
            };
        }

        public static BuiltInStorage ToRamFromCreateRamRequestDto(this CreateBuiltInStorageRequestDto createRamRequestDto)
        {
            return new BuiltInStorage
            {
                Capacity = createRamRequestDto.Capacity,
                Unit = createRamRequestDto.Unit
            };
        }
    }
}
