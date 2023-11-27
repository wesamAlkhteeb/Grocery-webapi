using Grocery.Application.Services.interfaces;
using Grocery.Domain.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Grocery.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult> GetProducts(int page) {
            return Ok(await this.productService.GetProduct(page));   
        }

        [HttpGet("/byid/{id}")]
        public async Task<ActionResult> GetProductById(string id)
        {
            return Ok(await this.productService.GetProductById(id));
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(AddProductModel addProductModel)
        {
            return Ok(await this.productService.AddProduct(addProductModel));
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateProduct(UpdateProductModel updateProductModel)
        {
            return Ok(await this.productService.UpdateProduct(updateProductModel));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> UpdateProduct(string id)
        {
            return Ok(await this.productService.DeleteProduct(id));
        }
    }
}
