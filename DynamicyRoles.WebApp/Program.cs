using DynamicyRoles.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Appsettings

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)

//e�er buradaki json file � bulursa yukar�dakini okumaz, yoksa yukar�dakine bakar
.AddJsonFile($"appsettings.{env}.json", optional: true)
.AddEnvironmentVariables();

var config = configuration.Build();

#endregion

builder.Services.AddPersistenceServices(config);

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
