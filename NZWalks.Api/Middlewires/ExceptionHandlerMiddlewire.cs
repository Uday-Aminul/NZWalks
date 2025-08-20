using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NZWalks.Middlewires
{
    public class ExceptionHandlerMiddlewire
    {
        private readonly ILogger<ExceptionHandlerMiddlewire> logger;
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddlewire(ILogger<ExceptionHandlerMiddlewire> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var id = Guid.NewGuid();
                logger.LogError(ex, $"{id} : {ex.Message}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var error = new
                {
                    Id = id,
                    Messege = ex.Message
                };
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }

    public static class ExceptionHandlerMiddlewireExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddlewire(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddlewire>();
        }
    }
}