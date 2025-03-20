using AutoMapper;
using Microsoft.AspNetCore.Http;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.ResponseDTOs;
using StockTracker.Shared.DTOs.WarehouseAccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Concrete
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

  
        public async Task<ResponseDTO<string>> AddIncomingTransactionAsync(CreateIncomingTransactionDTO incomingTransactionDTO)
        {
            var incomingTransaction = _mapper.Map<IncomingTransaction>(incomingTransactionDTO);
            await _unitOfWork.GetRepository<IncomingTransaction>().AddAsync(incomingTransaction);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDTO<string>.Success("Gelen işlem başarıyla eklendi.", StatusCodes.Status200OK);
        }

        public async Task<ResponseDTO<string>> AddOutgoingTransactionAsync(CreateOutgoingTransactionDTO outgoingTransactionDTO)
        {
            var outgoingTransaction = _mapper.Map<OutgoingTransaction>(outgoingTransactionDTO);
            await _unitOfWork.GetRepository<OutgoingTransaction>().AddAsync(outgoingTransaction);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDTO<string>.Success("Giden işlem başarıyla eklendi.", StatusCodes.Status200OK);
        }

  
        public async Task<ResponseDTO<decimal>> GetNetProfitAsync()
        {
      
            var currentDate = DateTime.UtcNow;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;

            var incomingTransactions = await _unitOfWork.GetRepository<IncomingTransaction>()
                .GetAllAsync(i => i.TransactionDate.Month == currentMonth && i.TransactionDate.Year == currentYear);

            var outgoingTransactions = await _unitOfWork.GetRepository<OutgoingTransaction>()
                .GetAllAsync(o => o.TransactionDate.Month == currentMonth && o.TransactionDate.Year == currentYear);

            decimal totalIncoming = incomingTransactions.Sum(i => i.Amount);
            decimal totalOutgoing = outgoingTransactions.Sum(o => o.Amount);

            decimal netProfit = totalIncoming - totalOutgoing;

            return ResponseDTO<decimal>.Success(netProfit, StatusCodes.Status200OK);
        }

        public async Task<ResponseDTO<IEnumerable<IncomingTransactionDTO>>> GetAllIncomingTransactionDTO()
        {

            var incomingTransactions = await _unitOfWork.GetRepository<IncomingTransaction>().GetAllAsync();
            if (incomingTransactions == null || !incomingTransactions.Any())
            {
                return ResponseDTO<IEnumerable<IncomingTransactionDTO>>.Fail("Hiç gelen işlem bulunamadı.", StatusCodes.Status404NotFound);
            }

            var incomingTransactionDTOs = _mapper.Map<IEnumerable<IncomingTransactionDTO>>(incomingTransactions);

            return ResponseDTO<IEnumerable<IncomingTransactionDTO>>.Success(incomingTransactionDTOs, StatusCodes.Status200OK);
        }

        public async Task<ResponseDTO<IEnumerable<OutgoingTransactionDTO>>> GetAllOutgoingTransactionDTO()
        {
            var outgoingTransactions = await _unitOfWork.GetRepository<OutgoingTransaction>().GetAllAsync();
            if (outgoingTransactions == null || !outgoingTransactions.Any())
            {
                return ResponseDTO<IEnumerable<OutgoingTransactionDTO>>.Fail("Hiç giden işlem bulunamadı.", StatusCodes.Status404NotFound);
            }

            var outgoingTransactionDTOs = _mapper.Map<IEnumerable<OutgoingTransactionDTO>>(outgoingTransactions);

            return ResponseDTO<IEnumerable<OutgoingTransactionDTO>>.Success(outgoingTransactionDTOs, StatusCodes.Status200OK);
        } 
    }
    }
