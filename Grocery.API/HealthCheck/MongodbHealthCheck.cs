using Grocery.Domain.Entity;
using Grocery.Infrastructure.DatabaseContext;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace Grocery.API.HealthCheck
{
    public class MongodbHealthCheck : IHealthCheck
    {
        IConfiguration _configurationManager;
        
        public MongodbHealthCheck(IConfiguration configuration) { 
            this._configurationManager = configuration;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                IMongoClient client = new MongoClient(_configurationManager["connectionString"]!);
                IMongoDatabase db = client.GetDatabase(_configurationManager["dbName"]!);
                IMongoCollection<AccountEntity> account = db.GetCollection<AccountEntity>(CustomCollections.account.ToString());
                FilterDefinition<AccountEntity> filter = Builders<AccountEntity>.Filter.Eq(account => account.Phone, "");
                await account.Find(filter).SingleOrDefaultAsync();
                return HealthCheckResult.Healthy("it's healthy");
            }
            catch (Exception ex) {
                return HealthCheckResult.Unhealthy($"it's not healthy because the database is not run or there are some bug. => {ex.Message}");
            }
            
        }
    }
}
