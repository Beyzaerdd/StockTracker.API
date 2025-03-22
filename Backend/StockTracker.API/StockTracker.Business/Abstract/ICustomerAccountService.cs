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
        Task<ResponseDTO<UpdateCustomerAccountDTO>> UpdateCustomerAccountAsync(UpdateCustomerAccountDTO updateCustomerAccountDTO);
        Task<ResponseDTO<string>> DeleteCustomerAccountAsync(int id);
        Task<ResponseDTO<CustomerAccountDTO>> GetCustomerAccountByIdAsync(int id);
        Task<ResponseDTO<List<CustomerAccountDTO>>> GetAllCustomerAccountsAsync(int? take = null);
        Task<ResponseDTO<IEnumerable<CustomerAccountDTO>>> GetCustomerAccountsAsync(int customerId);

    }
}
