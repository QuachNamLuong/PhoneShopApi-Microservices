using PhoneShopApi.Product.Dto.Cart;
using PhoneShopApi.Product.Dto.Order;
using PhoneShopApi.Product.Dto.BuiltInStorage;
using PhoneShopApi.Product.Dto.Phone;
using PhoneShopApi.Product.Models;
using PhoneShopApi.Product.Dto.Phone.Option;
using PhoneShopApi.Product.Dto.Phone.Color;
using PhoneShopApi.Product.Dto.Phone.Detail;

namespace PhoneShopApi.Product.Mappers
{
    public static class PhoneMapper
    {
        public static PhoneDto ToPhoneDto(this Phone phoneModel)
        {
            return new PhoneDto
            {
                Id = phoneModel.Id,
                Name = phoneModel.Name,
                BrandId = phoneModel.BrandId,
                IsSelling = phoneModel.IsSelling,
            };
        }

        public static Phone ToPhoneFromCreatePhoneRequestDto(this CreatePhoneRequestDto upCreatePhoneDto)
        {
            return new Phone
            {
                Name = upCreatePhoneDto.Name,
                BrandId = upCreatePhoneDto.BrandId,
                IsSelling = upCreatePhoneDto.IsSelling
            };
        }

        public static PhoneDetailDto ToPhoneDetailDto(this PhoneDetail phoneDetailModel)
        {
            return new PhoneDetailDto
            {
                Id = phoneDetailModel.Id,
                BackCamera = phoneDetailModel.BackCamera,
                Description = phoneDetailModel.Description,
                Device = phoneDetailModel.Device,
                Memory = phoneDetailModel.Memory,
                FronCamera = phoneDetailModel.FronCamera,
                PinAndCharger = phoneDetailModel.PinAndCharger,
                Connection = phoneDetailModel.Connection,
                OsAndCpu = phoneDetailModel.OsAndCpu,
                Screen = phoneDetailModel.Screen,
                Sound = phoneDetailModel.Sound,
            };
        }

        public static PhoneDetail ToPhoneDetailFromCreatePhoneDetailRequestDto(
            this CreatePhoneDetailRequestDto createPhoneDetailRequestDto)
        {
            return new PhoneDetail
            {
                Description = createPhoneDetailRequestDto.Description,
                Device = createPhoneDetailRequestDto.Device,
                Memory = createPhoneDetailRequestDto.Memory,
                FronCamera = createPhoneDetailRequestDto.FronCamera,
                BackCamera = createPhoneDetailRequestDto.BackCamera,
                PinAndCharger = createPhoneDetailRequestDto.PinAndCharger,
                Connection = createPhoneDetailRequestDto.Connection,
                OsAndCpu = createPhoneDetailRequestDto.OsAndCpu,
                Screen = createPhoneDetailRequestDto.Screen,
                Sound = createPhoneDetailRequestDto.Sound
            };
        }

        public static PhoneOptionDto ToPhoneOptionDto(this PhoneOption phoneOptionModel)
        {
            return new PhoneOptionDto
            {
                Id = phoneOptionModel.Id,
                PhoneColorId = phoneOptionModel.PhoneColorId,
                PhoneId = phoneOptionModel.PhoneId,
                BuiltInStorageId = phoneOptionModel.BuiltInStorageId,
                Price = phoneOptionModel.Price,
                Quantity = phoneOptionModel.Quantity
            };
        }

        public static PhoneOption ToPhoneOptionFromCreatePhoneOptionRequestDto(
            this CreatePhoneOptionRequestDto createPhoneOptionRequestDto)
        {
            return new PhoneOption
            {
                PhoneColorId = createPhoneOptionRequestDto.PhoneColorId,
                PhoneId = createPhoneOptionRequestDto.PhoneId,
                BuiltInStorageId = createPhoneOptionRequestDto.BuiltInStorageId,
                Price = createPhoneOptionRequestDto.Price,
                Quantity = createPhoneOptionRequestDto.Quantity
            };
        }

        public static PhoneColorDto ToPhoneColorDto(this PhoneColor phoneColorModel)
        {
            return new PhoneColorDto
            {
                Id = phoneColorModel.Id,
                Name = phoneColorModel.Name,
                ImageUrl = phoneColorModel.ImageUrl
            };
        }

        public static PhoneColor ToPhoneColorFromCreatePhoneColorRequestDto(
            this CreatePhoneColorRequestDto createPhoneColorRequestDto)
        {
            return new PhoneColor
            {
                Name = createPhoneColorRequestDto.Name,
            };
        }
    }
}
