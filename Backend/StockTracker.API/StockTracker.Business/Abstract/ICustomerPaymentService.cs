using StockTracker.Shared.DTOs.CustomerPaymentDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public interface ICustomerPaymentService
    {
        Task<ResponseDTO<CustomerPaymentDTO>> ReceivePaymentAsync(CustomerPaymentCreateDTO customerPaymentCreateDTO);
        Task<ResponseDTO<List<CustomerPaymentDTO>>> GetCustomerPaymentsAsync(int customerAccountId);
    }
}
