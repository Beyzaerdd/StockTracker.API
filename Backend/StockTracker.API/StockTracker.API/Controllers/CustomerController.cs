using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.CustomerDTOs;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }



        [HttpGet("allCustomers")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var response = await _customerService.GetAllCustomerAsync();
            return CreateResponse(response);
        }

        [HttpGet("getCustomerById/{id}")]

        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);
            return CreateResponse(response);
        }

        [HttpPost("createCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO createCustomerDTO)
        {
            var response = await _customerService.CreateCustomerAsync(createCustomerDTO);
            return CreateResponse(response);
        }

        [HttpPut("updateCustomer")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDTO updateCustomerDTO)
        {
            var response = await _customerService.UpdateCustomerAsync(updateCustomerDTO);
            return CreateResponse(response);
        }

        [HttpDelete("deleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var response = await _customerService.DeleteCustomerAsync(id);
            return CreateResponse(response);
        }


   

    }
}
