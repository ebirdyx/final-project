using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Data;

public interface ICatalogContext
{
    public IMongoCollection<Product> Products { get; }
}