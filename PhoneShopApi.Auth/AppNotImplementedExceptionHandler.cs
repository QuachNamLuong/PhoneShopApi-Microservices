using Microsoft.AspNetCore.Diagnostics;

namespace PhoneShopApi.Auth
{
    public class AppNotImplementedExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not NotImplementedException)
            {
                logger.LogError(exception, "Exception: {Message}", exception.Message);

                var reponse = new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status501NotImplemented,
                    Title = "Somthing went wrong",
                    ExceptionMessage = exception.Message
                };

                await httpContext.Response.WriteAsJsonAsync(reponse, cancellationToken);
                httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;

                return true;
            }

            return false;
        }
    }
}
