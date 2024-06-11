using PhoneShopApi.Dto.BuiltInStorage;
using PhoneShopApi.Models;

namespace PhoneShopApi.Mappers
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
