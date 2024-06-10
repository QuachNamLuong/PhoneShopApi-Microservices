using System.ComponentModel.DataAnnotations.Schema;



namespace PhoneShopApi.Product.Models
{
    public class PhoneDetail
    {
        public int Id { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; } = null!;
        [Column(TypeName = "text")]
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string Device { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string Screen { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string OsAndCpu { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string Memory { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string BackCamera { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string FronCamera { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string Sound { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string Connection { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        public string PinAndCharger { get; set; } = string.Empty;
    }
}
