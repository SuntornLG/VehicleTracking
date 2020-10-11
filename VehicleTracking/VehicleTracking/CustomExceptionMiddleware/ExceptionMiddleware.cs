using LoggerService;
using Microsoft.AspNetCore.Http;
using Service.ExceptionHandler;
using System;
using System.Net;
using System.Threading.Tasks;


namespace VehicleTracking.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(ServiceCustomException ex)
            {
                _logger.LogError($"An error occurred : {ex}");
                await HandleExceptionAsync(httpContext, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred : {ex}");
                await HandleExceptionAsync(httpContext);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, string message = "Internal Server Error.")
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
