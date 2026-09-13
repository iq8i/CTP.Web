using System.Net;
using System.Text.Json;

namespace CTP.Web.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ غير معالج في المسار {Path}", context.Request.Path);

                if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest"
                    || context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        success = false,
                        message = _env.IsDevelopment() ? ex.Message : "حدث خطأ غير متوقع"
                    }));
                }
                else
                {
                    context.Response.Redirect("/Home/Error");
                }
            }
        }
    }
}