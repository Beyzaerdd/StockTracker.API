using StockTracker.Shared.DTOs.EmployeeDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public interface IEmployeeService
    {
        Task<ResponseDTO<EmployeeDTO>> CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO);
        Task<ResponseDTO<IEnumerable<EmployeeDTO>>> GetAllEmployeesAsync(int? take = null);
        Task<ResponseDTO<EmployeeDTO>> GetEmployeeByIdAsync(int id);
        Task<ResponseDTO<UpdateEmployeeDTO>> UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployeeDTO);
        Task<ResponseDTO<string>> DeleteEmployeeAsync(int id);
    }
}
