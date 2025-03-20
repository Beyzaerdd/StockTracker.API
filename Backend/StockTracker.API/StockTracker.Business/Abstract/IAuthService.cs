using StockTracker.Shared.DTOs.ResponseDTOs;
using StockTracker.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracker.Shared.DTOs.EmployeeDTOs;

namespace StockTracker.Business.Abstract
{
    public interface IAuthService
    {
        Task<ResponseDTO<TokenDTO>> LoginAsync(LoginEmployeeDTO loginEmployeeDTO);
    }
}
