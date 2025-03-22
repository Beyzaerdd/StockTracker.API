using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Business.Concrete;
using StockTracker.Shared.DTOs.AccountTransactionDTOs;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : CustomControllerBase
    {
        private readonly ICustomerAccountService _customerAccountService;

        public CustomerAccountController(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }



        [HttpPost("createCustomerAccount")]
        public async Task<IActionResult> CreateCustomerAccount(CreateCustomerAccountDTO createCustomerAccountDTO)
        {
            var response = await _customerAccountService.CreateCustomerAccountAsync(createCustomerAccountDTO);
            return CreateResponse(response);
        }

        [HttpPut("updateCustomerAccount")]
        public async Task<IActionResult> UpdateCustomerAccount([FromBody] UpdateCustomerAccountDTO updateCustomerAccountDTO)
        {
            var response = await _customerAccountService.UpdateCustomerAccountAsync(updateCustomerAccountDTO);
            return CreateResponse(response);
        }

        [HttpDelete("deleteCustomerAccount/{id}")]
        public async Task<IActionResult> DeleteCustomerAccount(int id)
        {
            var response = await _customerAccountService.DeleteCustomerAccountAsync(id);
            return CreateResponse(response);
        }

        [HttpGet("getCustomerAccountById/{id}")]
        public async Task<IActionResult> GetCustomerAccountById(int id)
        {
            var response = await _customerAccountService.GetCustomerAccountByIdAsync(id);
            return CreateResponse(response);
        }


        [HttpGet("allCustomerAccounts")]
        public async Task<IActionResult> GetAllCustomerAccounts([FromQuery] int? take = null)
        {
            var response = await _customerAccountService.GetAllCustomerAccountsAsync(take);
            return CreateResponse(response);
        }

        [HttpGet("getCustomerAccounts/{customerId}")]
        public async Task<IActionResult> GetCustomerAccounts(int customerId)
        {
            var response = await _customerAccountService.GetCustomerAccountsAsync(customerId);
            return CreateResponse(response);
        }

    }
}
