using StockTracker.Shared.DTOs.ResponseDTOs;
using StockTracker.Shared.DTOs.WarehouseAccountDTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public interface ITransactionService
    {
        Task<ResponseDTO<string>> AddIncomingTransactionAsync(CreateIncomingTransactionDTO incomingTransactionDTO);
        Task<ResponseDTO<string>> AddOutgoingTransactionAsync(CreateOutgoingTransactionDTO outgoingTransactionDTO);
        Task<ResponseDTO<decimal>> GetNetProfitAsync();

        Task<ResponseDTO<IEnumerable<IncomingTransactionDTO>>> GetAllIncomingTransactionDTO();
        Task<ResponseDTO<IEnumerable<OutgoingTransactionDTO>>> GetAllOutgoingTransactionDTO();
    }
}
