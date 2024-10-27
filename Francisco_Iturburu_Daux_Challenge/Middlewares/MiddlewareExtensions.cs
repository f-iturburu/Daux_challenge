using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

namespace Francisco_Iturburu_Daux_Challenge.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UsePathRedirects(this IApplicationBuilder app, Dictionary<string, string> redirects)
        {
            return app.UseMiddleware<RedirectMiddleware>(redirects);
        }

        public static IApplicationBuilder UseLogger(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggerMiddleware>();
        }
    }
}