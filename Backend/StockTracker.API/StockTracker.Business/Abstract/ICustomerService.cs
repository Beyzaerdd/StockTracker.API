using StockTracker.Shared.DTOs;
using StockTracker.Shared.DTOs.AccountTransactionDTOs;
using StockTracker.Shared.DTOs.CustomerDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public interface ICustomerService
    {

        Task<ResponseDTO<IEnumerable<CustomerDTO>>> GetAllCustomerAsync();
        Task<ResponseDTO<CustomerDTO>> GetCustomerByIdAsync(int id);
        Task<ResponseDTO<CreateCustomerDTO>> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO);
        Task<ResponseDTO<UpdateCustomerDTO>> UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO);
        Task<ResponseDTO<CustomerDTO>> DeleteCustomerAsync(int id);
      
        



        }
}
