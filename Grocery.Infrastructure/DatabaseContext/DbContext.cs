
using Grocery.Domain.Entity;

using MongoDB.Driver;

namespace Grocery.Infrastructure.DatabaseContext
{
    public class DbContext
    {
        public IMongoCollection<AccountEntity> accountCollection { get; set; }
        public IMongoCollection<ProductEntity> productCollection { get; set; }
        public DbContext(IMongoDatabase database) {
            accountCollection = database.GetCollection<AccountEntity>(CustomCollections.account.ToString());
            productCollection = database.GetCollection<ProductEntity>(CustomCollections.product.ToString());
        }
    }
}
