// File: Models/Product.cs
namespace ProductCatalogService.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }

    public int Stock { get; set; }

    public string ImageUrl { get; set; }
}
