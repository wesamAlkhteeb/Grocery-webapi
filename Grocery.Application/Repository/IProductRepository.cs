using Grocery.Domain.Models.Product;
using Grocery.Domain.Models.Response;
using Grocery.Domain.Entity;



namespace Grocery.Application.Repository
{
    public interface IProductRepository
    {
        Task<string> AddProduct(AddProductModel addProductModel);
        Task<List<ProductEntity >> GetProduct(int page);
        Task<string> UpdateProduct(UpdateProductModel updateProductModel);
        Task<string> DeleteProduct(string productId);
        Task<ProductEntity> GetProductById(string productId);
    }
}
