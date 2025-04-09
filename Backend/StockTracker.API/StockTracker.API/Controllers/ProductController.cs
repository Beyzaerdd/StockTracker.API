using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.ProductDTOs;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("allProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProductsAsync();
            return CreateResponse(response);
        }

        [HttpGet("getProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            return CreateResponse(response);
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            var response = await _productService.CreateProductAsync(createProductDTO);
            return CreateResponse(response);
        }

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            var response = await _productService.UpdateProductAsync(updateProductDTO);
            return CreateResponse(response);
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProductAsync(id);
            return CreateResponse(response);
        }


        [HttpGet("getProductStockInfo")]
        public async Task<IActionResult> GetProductStockInfo()
        {
            var response = await _productService.GetProductStockInfoAsync();
            return CreateResponse(response);
        }


    }
}
