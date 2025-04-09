using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.RentalDTOs;
using StockTracker.Shared.DTOs.RentalItemDTOs;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : CustomControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("allRentals")]
        public async Task<IActionResult> GetAllRentals([FromQuery] int? take = null)
        {
            var response = await _rentalService.GetAllRentalsAsync(take);
            return CreateResponse(response);
        }

        [HttpGet("getRentalById/{id}")]
        public async Task<IActionResult> GetRentalById(int id)
        {
            var response = await _rentalService.GetRentalByIdAsync(id);
            return CreateResponse(response);
        }

        [HttpPost("createRental")]
        public async Task<IActionResult> CreateRental(CreateRentalDTO createRentalDTO)
        {
            var response = await _rentalService.CreateRentalAsync(createRentalDTO);
            return CreateResponse(response);
        }

        [HttpPut("updateRental/{rentalId}")]
        public async Task<IActionResult> UpdateRental([FromRoute] int rentalId, [FromBody] UpdateRentalDTO updateRentalDTO)
        {
            var response = await _rentalService.UpdateRentalAsync( rentalId, updateRentalDTO);
            return CreateResponse(response);
        }

        [HttpDelete("deleteRental/{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            var response = await _rentalService.DeleteRentalAsync(id);
            return CreateResponse(response);
        }

        [HttpPut("updateRentalItem/{rentalId}/{rentalItemId}")]
        public async Task<IActionResult> UpdateRentalItem([FromRoute] int rentalId, [FromRoute] int rentalItemId, [FromBody] UpdateRentalItemDTO updateRentalItemDTO)
        {
            var response = await _rentalService.UpdateRentalItemAsync(rentalId, rentalItemId, updateRentalItemDTO);
            return CreateResponse(response);
        }

        [HttpDelete("deleteRentalItem/{rentalId}/{rentalItemId}")]
        public async Task<IActionResult> DeleteRentalItem([FromRoute] int rentalId, [FromRoute] int rentalItemId)
        {
            var response = await _rentalService.DeleteRentalItemAsync(rentalId, rentalItemId);
            return CreateResponse(response);
        }

        [HttpGet("getRentalByCustomerId/{customerId}")]
        public async Task<IActionResult> GetRentalByCustomerId(int customerId)
        {
            var response = await _rentalService.GetRentalByCustomerId(customerId);
            return CreateResponse(response);
        }







    }
}
