using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShopApi.Dto.Phone.Detail
{
    public class UpdatePhoneDetailRequestDto
    {
        public string Description { get; set; } = string.Empty;
        public string Device { get; set; } = string.Empty;
        public string Screen { get; set; } = string.Empty;
        public string OsAndCpu { get; set; } = string.Empty;
        public string Memory { get; set; } = string.Empty;
        public string BackCamera { get; set; } = string.Empty;
        public string FronCamera { get; set; } = string.Empty;
        public string Sound { get; set; } = string.Empty;
        public string Connection { get; set; } = string.Empty;
        public string PinAndCharger { get; set; } = string.Empty;
    }
}
