using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Models.CustomerPaymentModels;

namespace StockTracker.MVC.Areas.Admin.Services.Abstract
{
    public interface ICustomerPaymentService
    {
        Task<ResponseViewModel<CustomerPaymentModel>> ReceivePaymentAsync(CustomerPaymentCreateModel customerPaymentCreateModel);
        Task<ResponseViewModel<IEnumerable<CustomerPaymentModel>>> GetCustomerPaymentsAsync(int customerAccountId);
    }
}
