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

        [HttpGet("netprofit")]
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

        [HttpGet("outgoing/{id}")]
        public async Task<IActionResult> GetOutgoingTransactionsById(int id)
        {
            var response = await _transactionService.GetOutgoingTransactionsById(id);
            return CreateResponse(response);
        }

        [HttpGet("incoming/{id}")]
        public async Task<IActionResult> GetIncomingTransactionsById(int id)
        {
            var response = await _transactionService.GetIncomingTransactionsById(id);
            return CreateResponse(response);
        }

        [HttpPut("incoming")]
        public async Task<IActionResult> UpdateIncomingTransaction([FromBody] UpdateIncomingTransactionDTO updateIncomingTransactionDTO)
        {
            var response = await _transactionService.UpdateIncomingTransactionAsync(updateIncomingTransactionDTO);
            return CreateResponse(response);
        }

        [HttpPut("outgoing")]
        public async Task<IActionResult> UpdateOutgoingTransaction([FromBody] UpdateOutgoingTransactionDTO updateOutgoingTransactionDTO)
        {
            var response = await _transactionService.UpdateOutgoingTransactionAsync(updateOutgoingTransactionDTO);
            return CreateResponse(response);
        }
    }
}
