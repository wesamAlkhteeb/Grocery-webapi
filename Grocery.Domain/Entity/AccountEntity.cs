
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Grocery.Domain.Entity
{
    public class AccountEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }
        public required string Code{ get; set; }
        public required bool IsConfirm{ get; set; }
        public required string Role { get; set; }
    }
}
