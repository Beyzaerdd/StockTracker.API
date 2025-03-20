using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.CustomerPaymentDTOs;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPaymentController : CustomControllerBase
    {
        private readonly ICustomerPaymentService _customerPaymentService;

        public CustomerPaymentController(ICustomerPaymentService customerPaymentService)
        {
            _customerPaymentService = customerPaymentService;
        }


        [HttpPost("receivepayment")]
        public async Task<IActionResult> ReceivePaymentAsync([FromBody] CustomerPaymentCreateDTO customerPaymentCreateDTO)
        {
            var response = await _customerPaymentService.ReceivePaymentAsync(customerPaymentCreateDTO);
            return CreateResponse(response);
        }

        [HttpGet("getcustomerpayments/{customerAccountId}")]
        public
            async Task<IActionResult> GetCustomerPaymentsAsync(int customerAccountId)
        {
            var response = await _customerPaymentService.GetCustomerPaymentsAsync(customerAccountId);
            return CreateResponse(response);
        }





    }




}
