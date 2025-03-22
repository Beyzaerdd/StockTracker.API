using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Models.CustomerAccountModels;

namespace StockTracker.MVC.Areas.Admin.Services.Abstract
{
    public interface ICustomerAccountService
    {
        // Müşteri hesabı oluşturma
        Task<ResponseViewModel<CreateCustomerAccountModel>> CreateCustomerAccountAsync(CreateCustomerAccountModel createCustomerAccountModel);

        // Müşteri hesabı güncelleme
        Task<ResponseViewModel<UpdateCustomerAccountModel>> UpdateCustomerAccountAsync(UpdateCustomerAccountModel updateCustomerAccountModel);

        // Müşteri hesabı silme
        Task<ResponseViewModel<CustomerAccountModel>> DeleteCustomerAccountAsync(int id);

        // Müşteri hesabı detaylarını alma
        Task<ResponseViewModel<CustomerAccountModel>> GetCustomerAccountByIdAsync(int id);

        // Tüm müşteri hesaplarını alma
        Task<ResponseViewModel<IEnumerable<CustomerAccountModel>>> GetAllCustomerAccountsAsync(int? take = null);

        // Müşteri bazında hesapları alma
        Task<ResponseViewModel<IEnumerable<CustomerAccountModel>>> GetCustomerAccountsAsync(int customerId);
    }
}
