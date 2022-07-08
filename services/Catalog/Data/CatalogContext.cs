using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        var databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
        
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        Products = database.GetCollection<Product>("Products");

        CatalogContextSeed.SeedData(Products);
    }
    
    public IMongoCollection<Product> Products { get; }
}