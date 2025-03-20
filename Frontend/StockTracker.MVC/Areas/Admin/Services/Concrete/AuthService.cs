using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Models;
using StockTracker.MVC.Areas.Admin.Models.AuthModel;
using StockTracker.MVC.Areas.Admin.Services.Abstract;

using System.Text.Json;

namespace StockTracker.MVC.Areas.Admin.Services.Concrete
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor) { }

        public async Task<ResponseViewModel<TokenModel>> LoginUserAsync(LoginUserModel userLoginModel)
        {
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync("auth/Login", userLoginModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                // Başarısız durumda, hata mesajını döndürüyoruz
                return ResponseViewModel<TokenModel>.Fail("Giriş işlemi başarısız. Lütfen tekrar deneyin.", StatusCodes.Status400BadRequest);
            }

            // Başarılı durumda, gelen responseBody'yi TokenModel'e dönüştürüp döndürüyoruz
            var tokenModel = JsonSerializer.Deserialize<TokenModel>(responseBody);

            if (tokenModel == null)
            {
                // Eğer tokenModel null dönerse, bir hata mesajı döndürüyoruz
                return ResponseViewModel<TokenModel>.Fail("Token oluşturulamadı.", StatusCodes.Status500InternalServerError);
            }

            return ResponseViewModel<TokenModel>.Success(tokenModel, StatusCodes.Status200OK);
        }
    }
}
