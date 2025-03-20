using StockTracker.Shared.DTOs.AccountTransactionDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public interface ICustomerAccountService
    {
        Task<ResponseDTO<string>> CreateCustomerAccountAsync(CreateCustomerAccountDTO createCustomerAccountDTO);
        Task<ResponseDTO<UpdateCustomerAccountDTO>> UpdateCustomerAccountAsync(int id, UpdateCustomerAccountDTO updateCustomerAccountDTO);
        Task<ResponseDTO<string>> DeleteCustomerAccountAsync(int id);
        Task<ResponseDTO<CustomerAccountDTO>> GetCustomerAccountByIdAsync(int id);
        Task<ResponseDTO<List<CustomerAccountDTO>>> GetAllCustomerAccountsAsync();
        Task<ResponseDTO<IEnumerable<CustomerAccountDTO>>> GetCustomerAccountsAsync(int customerId);

    }
}
