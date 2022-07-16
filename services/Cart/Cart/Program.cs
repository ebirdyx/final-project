using Cart.Repository;
using Cart.Services;
using Discount.gRPC.Protos;
using MassTransit;
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
builder.Services.AddScoped<DiscountService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    o => o.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")));

builder.Services.AddMassTransit(c =>
{
    c.UsingRabbitMq((context, config) =>
    {
        config.Host(
            builder.Configuration.GetValue<string>("Messaging:ConnectionString"));
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();

var app = builder.Build();

app.UsePathBase("/cart");

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
        c.SwaggerEndpoint("v1/swagger.json", "Cart v1");
    });
}

app.UseCors(c =>
{
    c.AllowAnyOrigin();
    c.AllowAnyHeader();
    c.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();