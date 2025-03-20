using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Models.AuthModel;
using StockTracker.MVC.Areas.Admin.Models;

namespace StockTracker.MVC.Areas.Admin.Services.Abstract
{
    public interface IAuthService
    {
        Task<ResponseViewModel<TokenModel>> LoginUserAsync(LoginUserModel userLoginModel);
    }
}
