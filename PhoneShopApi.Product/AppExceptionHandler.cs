using Microsoft.AspNetCore.Diagnostics;

namespace PhoneShopApi
{
    public class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler
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
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Somthing went wrong",
                    ExceptionMessage = exception.Message
                };

                await httpContext.Response.WriteAsJsonAsync(reponse, cancellationToken);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                return true;
            }

            return false;
        }
    }
}
