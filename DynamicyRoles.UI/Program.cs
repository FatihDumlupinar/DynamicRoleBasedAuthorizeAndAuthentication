using DynamicyRoles.Persistence.Extensions;
using DynamicyRoles.Persistence.Seed;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

#region Appsettings

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.AddJsonFile($"appsettings.{env}.json", optional: true)
.AddEnvironmentVariables();

var config = configuration.Build();

if (config == null)
{
    throw new NullReferenceException();
}

#endregion

builder.Services.AddPersistenceServices(config);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Account/Login";
    x.AccessDeniedPath = "/Errors/AccessDenied";
    x.LogoutPath = "/Account/Logout";

});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Errors/error-development");
}
else
{
    app.UseExceptionHandler("/Errors/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

await app.Services.SeedAsync();

app.Run();
