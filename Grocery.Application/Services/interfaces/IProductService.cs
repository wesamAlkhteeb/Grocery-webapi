using Grocery.Domain.Models.Product;
using Grocery.Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Application.Services.interfaces
{
    public interface IProductService
    {
        Task<ResponseSuccessModel> AddProduct(AddProductModel addProductModel);
        Task<ResponseSuccessModel> GetProduct(int page);
        Task<ResponseSuccessModel> UpdateProduct(UpdateProductModel updateProductModel);
        Task<ResponseSuccessModel> DeleteProduct(string productId);
        Task<ResponseSuccessModel> GetProductById(string productId);

    }
}
