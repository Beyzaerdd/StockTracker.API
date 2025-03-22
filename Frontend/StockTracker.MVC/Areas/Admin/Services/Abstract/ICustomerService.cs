using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Models.CustomerModels;

namespace StockTracker.MVC.Areas.Admin.Services.Abstract
{
    public interface ICustomerService
    {
        Task<ResponseViewModel<IEnumerable<CustomerModel>>> GetAllCustomerAsync(int count = 11);
        Task<ResponseViewModel<CustomerModel>> GetCustomerByIdAsync(int id);
        Task<ResponseViewModel<CreateCustomerModel>> CreateCustomerAsync(CreateCustomerModel createCustomerModel);
        Task<ResponseViewModel<UpdateCustomerModel>> UpdateCustomerAsync(UpdateCustomerModel updateCustomerModel);
        Task<ResponseViewModel<CustomerModel>> DeleteCustomerAsync(int id);
    }
}
