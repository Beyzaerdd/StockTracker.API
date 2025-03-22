using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Models.EmployeeModels;

namespace StockTracker.MVC.Areas.Admin.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<ResponseViewModel<CreateEmployeeModel>> CreateEmployeeAsync(CreateEmployeeModel createEmployeeModel);
        Task<ResponseViewModel<IEnumerable<EmployeeModel>>> GetAllEmployeesAsync(int count = 11);
        Task<ResponseViewModel<EmployeeModel>> GetEmployeeByIdAsync(int id);
        Task<ResponseViewModel<UpdateEmployeeModel>> UpdateEmployeeAsync(UpdateEmployeeModel updateEmployeeModel);
        Task<ResponseViewModel<string>> DeleteEmployeeAsync(int id);
    }
}
