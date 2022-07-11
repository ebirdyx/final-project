using MassTransit;
using Microsoft.OpenApi.Models;
using Orders.Application;
using Orders.Infrastructure;
using Orders.Infrastructure.Persistence;
using Orders.REST.Consumers;
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

builder.Services.AddMassTransit(c =>
{
    c.AddConsumer<CartCheckoutConsumer>();
    
    c.UsingRabbitMq((context, config) =>
    {
        config.Host(
            builder.Configuration.GetValue<string>("Messaging:ConnectionString"));
        
        config.ReceiveEndpoint(Messaging.Constants.CartCheckoutQueue, o =>
        {
            o.ConfigureConsumer<CartCheckoutConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<CartCheckoutConsumer>();

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
        c.SwaggerEndpoint("v1/swagger.json", "Orders v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();