using Catalog.Data;
using Catalog.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog", Version = "v1" });
});

builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddCors();

var app = builder.Build();

app.UsePathBase("/catalog");

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
        c.SwaggerEndpoint("v1/swagger.json", "Catalog v1");
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