using Francisco_Iturburu_Daux_Challenge.Constants;
using Francisco_Iturburu_Daux_Challenge.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Models;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("auth", (serviceProvider, client) =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Cookie", "HttpOnly");
    var BaseUrl = builder.Configuration["ApiBaseUrl"];

    if (!string.IsNullOrEmpty(BaseUrl))
    {
        client.BaseAddress = new Uri(BaseUrl);
    }
});

builder.Services.AddScoped<IAsyncExceptionHandler, AsyncExceptionHandler>();
builder.Services.AddScoped<IAuthRequest, AuthRequest>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseLogger();
app.UseStatusCodePagesWithReExecute("/NotFound");
app.UsePathRedirects(RedirectConstants.Redirects);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Urls.Add("http://*:5143");
app.Run();