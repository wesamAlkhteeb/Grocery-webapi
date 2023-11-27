using Grocery.Application.Repository;
using Grocery.Application.Services.interfaces;
using Grocery.Domain.Entity;
using Grocery.Domain.Models.Product;
using Grocery.Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<ResponseSuccessModel> AddProduct(AddProductModel addProductModel)
        {
            string result = await this.productRepository.AddProduct(addProductModel);
            return new ResponseSuccessModel { 
                data = result
            };
        }

        public async Task<ResponseSuccessModel> DeleteProduct(string productId)
        {
            string result = await this.productRepository.DeleteProduct(productId);
            return new ResponseSuccessModel
            {
                data = result
            };
        }

        public async Task<ResponseSuccessModel> GetProduct(int page)
        {
            List<ProductEntity> result = await this.productRepository.GetProduct(page);
            return new ResponseSuccessModel
            {
                data = result
            }; 
        }

        public async Task<ResponseSuccessModel> GetProductById(string productId)
        {
            ProductEntity result = await this.productRepository.GetProductById(productId);
            return new ResponseSuccessModel
            {
                data = result
            }; 
        }

        public async Task<ResponseSuccessModel> UpdateProduct(UpdateProductModel updateProductModel)
        {
            string result = await this.productRepository.UpdateProduct(updateProductModel); 
            return new ResponseSuccessModel
            {
                data = result
            }; 
        }
    }
}
