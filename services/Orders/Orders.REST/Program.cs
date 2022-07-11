using Microsoft.OpenApi.Models;
using Orders.Application;
using Orders.Infrastructure;
using Orders.Infrastructure.Persistence;
using Orders.REST.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Version = "v1" });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    
    OrderContextSeed
        .SeedAsync(context, logger)
        .Wait();
});

app.UsePathBase("/orders");

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();