using Bogus;
using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Data;

public static class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        var productsExist = productCollection
            .Find(p => true)
            .Any();

        if (!productsExist)
        {
            productCollection.InsertManyAsync(GetPreconfiguredProducts());
        }
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        var categories = new Faker().Commerce.Categories(10);
        
        var faker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Summary, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.Category, f => f.PickRandom(categories))
            .RuleFor(p => p.ImageFile, f => f.Image.PlaceImgUrl());
        
        return faker.Generate(100);
    }
}