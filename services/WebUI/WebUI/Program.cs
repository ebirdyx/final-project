using WebUI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var auth0Settings = builder
    .Configuration.GetSection("Authentication:Auth0")
    .Get<Auth0Settings>();

builder.Services.AddSingleton(auth0Settings);

var servicesConfiguration = builder
    .Configuration.GetSection("ServicesConfiguration")
    .Get<ServicesConfiguration>();

builder.Services.AddSingleton(servicesConfiguration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();