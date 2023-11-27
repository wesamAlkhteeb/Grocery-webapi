using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Grocery.Domain.Entity
{
    public class ProductEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal price { get; set; }
    }
}
