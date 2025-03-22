using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.WarehouseAccountDTOs;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : CustomControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("incoming")]
        public async Task<IActionResult> AddIncomingTransactionAsync([FromBody] CreateIncomingTransactionDTO incomingTransactionDTO)
        {
            var response = await _transactionService.AddIncomingTransactionAsync(incomingTransactionDTO);
            return CreateResponse(response);
        }

        [HttpPost("outgoing")]
        public async Task<IActionResult> AddOutgoingTransactionAsync([FromBody] CreateOutgoingTransactionDTO outgoingTransactionDTO)
        {
            var response = await _transactionService.AddOutgoingTransactionAsync(outgoingTransactionDTO);
            return CreateResponse(response);
        }

        [HttpGet("net-profit")]
        public async Task<IActionResult> GetNetProfitAsync()
        {
            var response = await _transactionService.GetNetProfitAsync();
            return CreateResponse(response);
        }

        [HttpGet("Allincoming")]
        public async Task<IActionResult> GetAllIncomingTransaction([FromQuery] int? take = null)
        {
            var response = await _transactionService.GetAllIncomingTransaction(take);
            return CreateResponse(response);
        }

        [HttpGet("Alloutgoing")]
        public async Task<IActionResult> GetAllOutgoingTransaction([FromQuery] int? take = null)
        {
            var response = await _transactionService.GetAllOutgoingTransaction(take);
            return CreateResponse(response);
        }
    }
}
