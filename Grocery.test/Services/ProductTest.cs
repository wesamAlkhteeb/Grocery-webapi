
using Grocery.Application.Services.interfaces;
using Grocery.Application.Services;
using Grocery.Domain.Models.Product;
using Grocery.Domain.Models.Response;
using MongoDB.Driver;
using Grocery.Infrastructure.DatabaseContext;
using Grocery.Application.Repository;
using Grocery.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Grocery.Domain.Entity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Grocery.test.Services
{
    public class ProductTest
    {
        public IProductService GetProductService()
        {
            IMongoDatabase dbFake = (new MongoClient("mongodb://localhost:6066")).GetDatabase(Guid.NewGuid().ToString());
            DbContext db= new DbContext(dbFake);
            IProductRepository productRepository = new ProductRepository(db);
            return new ProductService(productRepository);
        }
        [Fact]
        public async Task AddProduct_AddUniqueNameForOneTime_ReturnSuccessfully()
        {

            // arrange 
            AddProductModel addProductModel = new AddProductModel
            {
                Name = "Botato",
                Description = "From ...",
                price = 50.5m
            };
            //act
            IProductService productService = GetProductService();
            var result = await productService.AddProduct(addProductModel);
            //assert
            Assert.IsType<ResponseSuccessModel>(result);
        }

        [Fact]
        public async Task AddProduct_AddExistsName_ThrowException()
        {
            // arrange 
            AddProductModel addProductModel = new AddProductModel
            {
                Name = "Botato",
                Description = "From ...",
                price = 50.5m
            };
            //act + assert

            IProductService productService = GetProductService();
            await productService.AddProduct(addProductModel);
            await Assert.ThrowsAsync<BadHttpRequestException>(async () => await productService.AddProduct(addProductModel));
        }

        [Fact]
        public async Task GetProduct_AddTwintyTwoProduct_GetTwoPageAndExceptionThird()
        {
            IProductService productService = GetProductService();
            // arrange 
            for(int i=0; i<22; i++)
            {
                AddProductModel addProductModel = new AddProductModel
                {
                    Name = $"Botato{i+1}",
                    Description = "From ...",
                    price = 50.5m
                };
                await productService.AddProduct(addProductModel);
            }
            //act
            var result1 = await productService.GetProduct(2);
            List<ProductEntity> data1 = result1.data as List<ProductEntity>?? new List<ProductEntity>();
            var result2 = await productService.GetProduct(3);
            List<ProductEntity> data2 = result2.data as List<ProductEntity> ?? new List<ProductEntity>();
            var result3 = await productService.GetProduct(4);
            List<ProductEntity> data3 = result3.data as List<ProductEntity> ?? new List<ProductEntity>();
            //assert
            Assert.Equal( 2, data2.Count );
            Assert.Equal( 10, data1.Count);
            Assert.Empty( data3 );
        }

        [Fact]
        public async Task Update_AddProductAndUpdateIt_Success()
        {
            IProductService productService = GetProductService();
            // arrange 
            AddProductModel addProductModel = new AddProductModel
            {
                Name = "Botato",
                Description = "From ...",
                price = 50.5m
            };
            await productService.AddProduct(addProductModel);
            var result= await productService.GetProduct(1);
            List<ProductEntity> data1 = result.data as List<ProductEntity> ?? new List<ProductEntity>();
            UpdateProductModel updateProduct = new UpdateProductModel
            {
                Id = data1.First().Id,
                Name = "Botatooos",
                Description = "From ...@@",
                price = 20.5m
            };
            //act
            await productService.UpdateProduct(updateProduct);
            var result1 = await productService.GetProduct(1);
            List<ProductEntity> data2 = result1.data as List<ProductEntity> ?? new List<ProductEntity>();
            //assert
            Assert.Equal("Botatooos",data2[0].Name);
            Assert.Equal("From ...@@", data2[0].Description);
            Assert.Equal(20.5m, data2[0].price);
        }

        [Fact]
        public async Task Update_UpdateProductNotFound_ThrowException()
        {
            IProductService productService = GetProductService();
            // arrange
            UpdateProductModel updateProduct = new UpdateProductModel
            {
                Id = "655c52d08e0ff2c4f00a558e",
                Name = "Botatooos",
                Description = "From ...@@",
                price = 20.5m
            };
            //act + assert
            await Assert.ThrowsAsync<BadHttpRequestException>(async () => await productService.UpdateProduct(updateProduct));
        }

        [Fact]
        public async Task Delete_DeleteProductNotFound_ThrowException()
        {
            IProductService productService = GetProductService();
            //act + assert
            await Assert.ThrowsAsync<BadHttpRequestException>(async () => await productService.DeleteProduct("655c52d08e0ff2c4f00a558e"));
        }


        [Fact]
        public async Task Delete_AddProductAndDeleteIt_Successfully()
        {
            IProductService productService = GetProductService();
            //arrange
            AddProductModel addProductModel = new AddProductModel
            {
                Name = "Botato",
                Description = "From ...",
                price = 50.5m
            };
            await productService.AddProduct(addProductModel);
            ResponseSuccessModel response = await productService.GetProduct(1);
            ProductEntity product = (response.data as List<ProductEntity> ?? new List<ProductEntity>())[0];
            //act
            await productService.DeleteProduct(product.Id);
            //assert
            await Assert.ThrowsAsync<BadHttpRequestException>(async () => await productService.GetProductById(productId:product.Id));
        }


        [Fact]
        public async Task GetProductById_AddProductAndGEtIdByID_Successfully()
        {
            IProductService productService = GetProductService();
            //arrange
            AddProductModel addProductModel = new AddProductModel
            {
                Name = "Botato",
                Description = "From ...",
                price = 50.5m
            };
            await productService.AddProduct(addProductModel);
            ResponseSuccessModel response = await productService.GetProduct(1);
            ProductEntity product = (response.data as List<ProductEntity> ?? new List<ProductEntity>())[0];
            //act
            ResponseSuccessModel responseProduct  = await productService.GetProductById(product.Id);
            //assert
            Assert.NotNull(responseProduct.data);
        }


        [Fact]
        public async Task GetProductById_GetAny_ThrowException()
        {
            IProductService productService = GetProductService();
            //act + assert
            await Assert.ThrowsAsync<BadHttpRequestException>(async () => await productService.GetProductById("655c52d08e0ff2c4f00a558e"));
        }
    }
}
