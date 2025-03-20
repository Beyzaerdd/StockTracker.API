using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Data.Concrete.Repositories;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.CustomerPaymentDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using StockTracker.Shared.DTOs.WarehouseAccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Concrete
{
    public class CustomerPaymentService :ICustomerPaymentService
    {
        private readonly IGenericRepository<CustomerPayment> _paymentRepository;
        private readonly IGenericRepository<CustomerAccount> _customerAccountRepository;
        private readonly ITransactionService transactionService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerPaymentService(IGenericRepository<CustomerPayment> paymentRepository, IGenericRepository<CustomerAccount> customerAccountRepository,  IMapper mapper, IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            _paymentRepository = paymentRepository;
            _customerAccountRepository = customerAccountRepository;
    
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            this.transactionService = transactionService;
        }
        public async Task<ResponseDTO<CustomerPaymentDTO>> ReceivePaymentAsync(CustomerPaymentCreateDTO customerPaymentCreateDTO)
        {
            var customerAccount = await _customerAccountRepository.GetAsync(
                  c => c.Id == customerPaymentCreateDTO.CustomerAccountId,
                  include => include.Include(ca => ca.Customer) 
              );
            if (customerAccount == null)
            {
                return ResponseDTO<CustomerPaymentDTO>.Fail("Müşteri hesabı bulunamadı.", StatusCodes.Status404NotFound);
            }

         
            if (customerAccount.Customer == null)
            {
                return ResponseDTO<CustomerPaymentDTO>.Fail("Müşteri bilgileri bulunamadı.", StatusCodes.Status404NotFound);
            }

        
            var payment = _mapper.Map<CustomerPayment>(customerPaymentCreateDTO);
            await _paymentRepository.AddAsync(payment);

          
            customerAccount.PaidAmount += payment.Amount;
            _customerAccountRepository.Update(customerAccount);

          
            var incomingTransaction = new CreateIncomingTransactionDTO
            {
                Amount = payment.Amount, 
                Description = $"Ödeme: {customerAccount.Customer.Name}", 
                TransactionDate = payment.PaymentDate 
            };

         
            await transactionService.AddIncomingTransactionAsync(incomingTransaction);

    
            var paymentDTO = _mapper.Map<CustomerPaymentDTO>(payment);
            return ResponseDTO<CustomerPaymentDTO>.Success(paymentDTO, StatusCodes.Status201Created);
        }


        public async Task<ResponseDTO<List<CustomerPaymentDTO>>> GetCustomerPaymentsAsync(int customerAccountId)
        {
            var payments = await _paymentRepository.GetAllAsync(p => p.CustomerAccountId == customerAccountId);
            var paymentDTOs = _mapper.Map<List<CustomerPaymentDTO>>(payments);
            return ResponseDTO<List<CustomerPaymentDTO>>.Success(paymentDTOs, StatusCodes.Status200OK);
        }
    }
}
