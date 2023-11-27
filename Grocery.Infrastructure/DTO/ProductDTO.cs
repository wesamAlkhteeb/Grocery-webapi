using Grocery.Domain.Models.Product;
using Grocery.Domain.Entity;

namespace Grocery.Infrastructure.DTO
{
    public static class ProductDTO
    {
        public static ProductEntity ConvertToEntity(this AddProductModel addProductModel)
        {
            return new ProductEntity {
                Name = addProductModel.Name,
                Description = addProductModel.Description,
                price = addProductModel.price
            };
        }
    }
}
