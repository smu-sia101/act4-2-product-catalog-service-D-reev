using ProductCatalogService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductCatalogService.Abellera.Models;

namespace ProductCatalogService.Services;

public class ProductService
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductService(
        IOptions<ProductsDatabaseSettings> productDatabaseSettings)
    {
        var settings = productDatabaseSettings.Value;

        var mongoClient = new MongoClient(settings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

        _productCollection = mongoDatabase.GetCollection<Product>(settings.ProductsCollectionName);
    }

    public async Task<List<Product>> GetAsync() =>
        await _productCollection.Find(_ => true).ToListAsync();

    public async Task<Product?> GetAsync(string id) =>
        await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Product newProduct) =>
        await _productCollection.InsertOneAsync(newProduct);

    public async Task UpdateAsync(string id, Product updatedProduct) =>
        await _productCollection.ReplaceOneAsync(x => x.Id == id, updatedProduct);

    public async Task RemoveAsync(string id) =>
        await _productCollection.DeleteOneAsync(x => x.Id == id);
}
