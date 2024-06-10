namespace PhoneShopApi
{
    public class ErrorReponse
    {
        public int StatusCode { get; set; }
        public string Title { get; set; } = null!;
        public string ExceptionMessage { get; set; } = null!;
    }
}
