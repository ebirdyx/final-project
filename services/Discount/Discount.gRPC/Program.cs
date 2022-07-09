using Discount.Common.Configuration;
using Discount.Common.Repository;
using Discount.gRPC.Extensions;
using Discount.gRPC.Services;
using Discount.Repository;

var builder = WebApplication.CreateBuilder(args);

var databaseSettings = builder.Configuration
        .GetSection("DatabaseSettings")
        .Get<DatabaseSettings>();

builder.Services.AddSingleton(databaseSettings);
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddGrpc();

var app = builder.Build();

app.MigrateDatabase<Program>();
app.MapGrpcService<DiscountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();