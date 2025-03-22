using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.RentalDTOs;
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







    }
}
