using Microsoft.AspNetCore.Builder;

namespace TWAssessment.API.Common
{
    /// <summary>
    /// Custom exception handle middleware extension for registering into the pipeline
    /// </summary>
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
