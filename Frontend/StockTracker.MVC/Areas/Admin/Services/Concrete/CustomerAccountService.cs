using Newtonsoft.Json;

using StockTracker.MVC.Areas.Admin.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Models.CustomerAccountModels;

namespace StockTracker.MVC.Areas.Admin.Services.Concrete
{
    public class CustomerAccountService : BaseService, ICustomerAccountService
    {
        public CustomerAccountService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) :
            base(httpClientFactory, httpContextAccessor)
        { }


        public async Task<ResponseViewModel<CreateCustomerAccountModel>> CreateCustomerAccountAsync(CreateCustomerAccountModel createCustomerAccountModel)
        {
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync("customerAccount/createCustomerAccount", createCustomerAccountModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<CreateCustomerAccountModel>.Fail("Müşteri hesabı oluşturma işlemi başarısız oldu.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<CreateCustomerAccountModel>>(responseBody);

            return result ?? ResponseViewModel<CreateCustomerAccountModel>.Fail("Müşteri hesabı oluşturulurken bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<UpdateCustomerAccountModel>> UpdateCustomerAccountAsync(UpdateCustomerAccountModel updateCustomerAccountModel)
        {
            var client = GetHttpClient();
            var response = await client.PutAsJsonAsync("customerAccount/updateCustomerAccount", updateCustomerAccountModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<UpdateCustomerAccountModel>.Fail("Müşteri hesabı güncelleme işlemi başarısız oldu.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<UpdateCustomerAccountModel>>(responseBody);

            return result ?? ResponseViewModel<UpdateCustomerAccountModel>.Fail("Müşteri hesabı güncellenirken bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

   
        public async Task<ResponseViewModel<CustomerAccountModel>> DeleteCustomerAccountAsync(int id)
        {
            var client = GetHttpClient();
            var response = await client.DeleteAsync($"customerAccount/deleteCustomerAccount/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<CustomerAccountModel>.Fail("Müşteri hesabı silme işlemi başarısız oldu.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<CustomerAccountModel>>(responseBody);

            return result ?? ResponseViewModel<CustomerAccountModel>.Fail("Müşteri hesabı silinirken bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

 
        public async Task<ResponseViewModel<CustomerAccountModel>> GetCustomerAccountByIdAsync(int id)
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<CustomerAccountModel>>($"customerAccount/getCustomerAccountById/{id}");

            if (response == null || !response.success)
            {
                return ResponseViewModel<CustomerAccountModel>.Fail("Müşteri hesabı verileri alınırken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return response;
        }

    
        public async Task<ResponseViewModel<IEnumerable<CustomerAccountModel>>> GetAllCustomerAccountsAsync(int? take = null)
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<IEnumerable<CustomerAccountModel>>>($"customerAccount/allCustomerAccounts?take={take}");

            if (response == null || !response.success)
            {
                return ResponseViewModel<IEnumerable<CustomerAccountModel>>.Fail("Müşteri hesapları alınırken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return response;
        }

     
        public async Task<ResponseViewModel<IEnumerable<CustomerAccountModel>>> GetCustomerAccountsAsync(int customerId)
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<IEnumerable<CustomerAccountModel>>>($"customerAccount/getCustomerAccounts/{customerId}");

            if (response == null || !response.success)
            {
                return ResponseViewModel<IEnumerable<CustomerAccountModel>>.Fail("Müşteri hesapları alınırken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}
