using Grocery.Application.Repository;
using Grocery.Domain.Entity;
using Grocery.Domain.Models.Product;
using Grocery.Domain.Models.Response;
using Grocery.Infrastructure.DatabaseContext;
using Grocery.Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext dbContext;

        public ProductRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> AddProduct(AddProductModel addProductModel)
        {
            FilterDefinition<ProductEntity> filter =
                    Builders<ProductEntity>.Filter
                        .Eq(product => product.Name, addProductModel.Name);
            var data = await this.dbContext.productCollection.Find(filter).ToListAsync();
            if(data.Count > 0)
            {
                throw new BadHttpRequestException("Product name is used.");
            }
            await this.dbContext.productCollection.InsertOneAsync(addProductModel.ConvertToEntity());
            return "Product is Added";
        }

        public async Task<string> DeleteProduct(string productId)
        {
            FilterDefinition<ProductEntity> filter =
               Builders<ProductEntity>
                   .Filter
                   .Eq(product => product.Id, productId);
            DeleteResult dr = await dbContext.productCollection.DeleteOneAsync(filter);
            if (dr.DeletedCount == 0)
            {
                throw new BadHttpRequestException("Product is not found to delete it.");
            }
            return "Delete successfully.";
        }

        public async Task<List<ProductEntity>> GetProduct(int page)
        {
            return await dbContext.productCollection
                .Find(Builders<ProductEntity>.Filter.Empty)
                .Skip((page - 1) * 10).Limit(10).ToListAsync();

        }

        public async Task<ProductEntity> GetProductById(string productId)
        {
            FilterDefinition<ProductEntity> filter =
               Builders<ProductEntity>
                   .Filter
                   .Eq(product => product.Id, productId);
            ProductEntity product = await dbContext.productCollection.Find(filter).FirstOrDefaultAsync();
            if(product == null)
            {
                throw new BadHttpRequestException("Product is not found.");
            } 
            return product;
        }

        public async Task<string> UpdateProduct(UpdateProductModel updateProductModel)
        {
            FilterDefinition<ProductEntity> filter = 
                Builders<ProductEntity>
                    .Filter
                    .Eq(product => product.Id, updateProductModel.Id);
            UpdateDefinition<ProductEntity> update=Builders<ProductEntity>.Update
                    .Set(account => account.Name, updateProductModel.Name)
                    .Set(account => account.Description, updateProductModel.Description)
                    .Set(account => account.price, updateProductModel.price);
            UpdateResult ur = await dbContext.productCollection.UpdateOneAsync(filter, update);
            if (ur.ModifiedCount==0)
            {
                throw new BadHttpRequestException("this Product is not found.");
            }
            return "Update is successfully.";
        }
    }
}
