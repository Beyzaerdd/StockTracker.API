using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.RemainingProductDTO;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemainingProductController : CustomControllerBase
    {
        private readonly IRemainingProductService _remainingProductService;

        public RemainingProductController(IRemainingProductService remainingProductService)
        {
            _remainingProductService = remainingProductService;
        }

        [HttpGet("allRemainingProducts")]
        public async Task<IActionResult> GetAllRemainingProducts()
        {
            var response = await _remainingProductService.GetAllRemainingProductsAsync();
            return CreateResponse(response);
        }



        [HttpPut("updateRemainingProduct")]
        public async Task<IActionResult> UpdateRemainingProduct(UpdateRemainingProductDTO updateRemainingProductDTO)
        {
            var response = await _remainingProductService.UpdateRemainingProductAsync(updateRemainingProductDTO);
            return CreateResponse(response);
        }

        [HttpPost("processRemainingProducts/{rentalId}/{createNewRental}")]
        public async Task<IActionResult> ProcessRemainingProducts(int rentalId, bool createNewRental)
        {
            var response = await _remainingProductService.ProcessRemainingProductsAsync(rentalId, createNewRental);
            return CreateResponse(response);
        }

        [HttpGet("getRemainingProductsByRentalId/{rentalId}")]
        public async Task<IActionResult> GetRemainingProductsByRentalId(int rentalId)
        {
            var response = await _remainingProductService.GetRemainingProductsByRentalIdAsync(rentalId);
            return CreateResponse(response);
        }







    }
}
