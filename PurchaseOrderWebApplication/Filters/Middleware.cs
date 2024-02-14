using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace PurchaseOrderWebApplication.Filters
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        
        public Middleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("Middleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Middleware executing..");
            httpContext.Response.OnStarting(() =>
            {
                httpContext.Response.Headers["Message"] = $"Developed by Logha!";
                return Task.CompletedTask;
            });
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
