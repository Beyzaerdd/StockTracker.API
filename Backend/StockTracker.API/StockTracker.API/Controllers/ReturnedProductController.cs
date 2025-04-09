using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.DeliveredItemDTOs;
using StockTracker.Shared.DTOs.ReturnedProductDTOs;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnedProductController : CustomControllerBase
    {

        private readonly IReturnedProductService _returnedProductService;

        public ReturnedProductController(IReturnedProductService returnedProductService)
        {
            _returnedProductService = returnedProductService;
        }


        [HttpGet("allReturnedProducts")]
        public async Task<IActionResult> GetAllReturnedProducts()
        {
            var response = await _returnedProductService.GetAllReturnedProductsAsync();
            return CreateResponse(response);
        }

        [HttpGet("getReturnedProductById/{id}")]
        public async Task<IActionResult> GetReturnedProductById(int id)
        {
            var response = await _returnedProductService.GetReturnedProductByIdAsync(id);
            return CreateResponse(response);
        }

        [HttpPost("createReturnedProducts")]
        public async Task<IActionResult> CreateReturnedProducts(IEnumerable<CreateReturnedProductDTO> createReturnedProductDTOs)
        {
            var response = await _returnedProductService.CreateReturnedProductsAsync(createReturnedProductDTOs);
            return CreateResponse(response);
        }


        [HttpPut("updateReturnedProduct")]

        public async Task<IActionResult> UpdateReturnedProduct(UpdateReturnedProductDTO updateReturnedProductDTO)
        {
            var response = await _returnedProductService.UpdateReturnedProductAsync(updateReturnedProductDTO);
            return CreateResponse(response);
        }

        [HttpDelete("deleteReturnedProduct/{id}")]

        public async Task<IActionResult> DeleteReturnedProduct(int id)
        {
            var response = await _returnedProductService.DeleteReturnedProductAsync(id);
            return CreateResponse(response);
        }


        [HttpGet("getReturnedProductsByRentalId/{rentalId}")]
        public
            async Task<IActionResult> GetReturnedProductsByRentalId(int rentalId)
        {
            var response = await _returnedProductService.GetReturnedProductsByRentalIdAsync(rentalId);
            return CreateResponse(response);
        }



    }
}
