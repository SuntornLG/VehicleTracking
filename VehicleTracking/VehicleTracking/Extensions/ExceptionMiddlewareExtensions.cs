
using Microsoft.AspNetCore.Builder;
using VehicleTracking.CustomExceptionMiddleware;

namespace VehicleTracking.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
