using Newtonsoft.Json;
using StockTracker.MVC.Areas.Admin.Models.CustomerPaymentModels;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Services.Abstract;

namespace StockTracker.MVC.Services.CustomerPayments
{
    public class CustomerPaymentService : BaseService, ICustomerPaymentService
    {
        public CustomerPaymentService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor) { }

        public async Task<ResponseViewModel<CustomerPaymentModel>> ReceivePaymentAsync(CustomerPaymentCreateModel customerPaymentCreateModel)
        {
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync("customerpayment/receivepayment", customerPaymentCreateModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<CustomerPaymentModel>.Fail("Ödeme alınırken hata oluştu. Lütfen tekrar deneyin.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<CustomerPaymentModel>>(responseBody);

            return result ?? ResponseViewModel<CustomerPaymentModel>.Fail("Ödeme alınırken beklenmeyen bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<IEnumerable<CustomerPaymentModel>>> GetCustomerPaymentsAsync(int customerAccountId)
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<List<CustomerPaymentModel>>>($"customerpayment/getcustomerpayments/{customerAccountId}");

            if (response == null || !response.success)
            {
                return ResponseViewModel<IEnumerable<CustomerPaymentModel>>.Fail("Ödeme geçmişi alınırken hata oluştu.", StatusCodes.Status500InternalServerError);
            }

          
            return ResponseViewModel<IEnumerable<CustomerPaymentModel>>.Success(response.Data.AsEnumerable(), StatusCodes.Status200OK);
        }

    }
}
