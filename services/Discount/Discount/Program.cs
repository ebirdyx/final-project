using Discount.Common.Configuration;
using Discount.Common.Repository;
using Discount.Extensions;
using Discount.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Discount", Version = "v1" });
});

var databaseSettings = builder.Configuration
        .GetSection("DatabaseSettings")
        .Get<DatabaseSettings>();

builder.Services.AddSingleton(databaseSettings);

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

var app = builder.Build();

app.UsePathBase("/discount");

app.MigrateDatabase<Program>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swaggerDoc, request) =>
        {
            const string prefixHeader = "X-Forwarded-Prefix";
            if (!request.Headers.ContainsKey(prefixHeader))
                return;
            
            var serverUrl = request.Headers[prefixHeader];
            swaggerDoc.Servers = new List<OpenApiServer>
            {
                new() { Description = "Server behind nginx", Url = serverUrl }
            };
        });
    });
    
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();