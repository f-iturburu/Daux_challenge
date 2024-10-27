namespace Francisco_Iturburu_Daux_Challenge.Middlewares;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RedirectMiddleware(RequestDelegate next, Dictionary<string, string> redirects)
{
    private readonly RequestDelegate _next = next;
    private readonly Dictionary<string, string> _redirects = redirects;

    public async Task InvokeAsync(HttpContext context)
    {
        if (_redirects.TryGetValue(context.Request.Path, out string redirectPath))
        {
            context.Response.Redirect(redirectPath);
            return;
        }

        await _next(context);
    }
}