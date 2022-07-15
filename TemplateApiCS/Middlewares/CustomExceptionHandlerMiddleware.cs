using Microsoft.AspNetCore.Diagnostics;

namespace TemplateApiCS.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> Logger;
        private readonly RequestDelegate Next;
        public CustomExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> Logger)
        {
            this.Logger = Logger;
            this.Next = Next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(exception: ex, "Uncaught Exception");
                throw;
            }
        }
    }
}
