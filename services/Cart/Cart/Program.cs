using Cart.Repository;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cart", Version = "v1" });
});

builder.Services.AddStackExchangeRedisCache(c =>
{
    c.ConfigurationOptions = ConfigurationOptions.Parse(
        builder.Configuration.GetValue<string>("CacheSettings:ConnectionString"));
    c.ConfigurationOptions.Password = 
        builder.Configuration.GetValue<string>("CacheSettings:Password");
});

builder.Services.AddScoped<ICartRepository, CartRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();