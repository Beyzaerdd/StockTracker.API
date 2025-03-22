using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using Newtonsoft.Json;
using StockTracker.MVC.Areas.Admin.Models.CustomerModels;
using StockTracker.MVC.Areas.Admin.Services.Abstract;

namespace StockTracker.MVC.Areas.Admin.Services.Concrete
{
    public class CustomerService: BaseService, ICustomerService
    {
        public CustomerService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) :
            base(httpClientFactory, httpContextAccessor)
        { }

        public async Task<ResponseViewModel<CreateCustomerModel>> CreateCustomerAsync(CreateCustomerModel createCustomerModel)
        {
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync("customer/createCustomer", createCustomerModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<CreateCustomerModel>.Fail("Müşteri oluşturma işlemi başarısız oldu. Lütfen tekrar deneyin.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<CreateCustomerModel>>(responseBody);

            if (result == null)
            {
                return ResponseViewModel<CreateCustomerModel>.Fail("Müşteri oluşturulurken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return result;
        }


        public async Task<ResponseViewModel<CustomerModel>> DeleteCustomerAsync(int id)
        {
            var client = GetHttpClient();
            var response = await client.DeleteAsync($"customer/deleteCustomer/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<CustomerModel>.Fail("Müşteri silme işlemi başarısız oldu. Lütfen tekrar deneyin.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<CustomerModel>>(responseBody);

            if (result == null)
            {
                return ResponseViewModel<CustomerModel>.Fail("Müşteri silinirken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return result;
        }

        public async Task<ResponseViewModel<IEnumerable<CustomerModel>>> GetAllCustomerAsync(int count = 11)
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<IEnumerable<CustomerModel>>>($"customer/allCustomers?take={count}");

            if (response == null || !response.success)
            {
                return ResponseViewModel<IEnumerable<CustomerModel>>.Fail("Müşteriler alınırken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<ResponseViewModel<CustomerModel>> GetCustomerByIdAsync(int id)
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<CustomerModel>>($"customer/getCustomerById/{id}");

            if (response == null || !response.success)
            {
                return ResponseViewModel<CustomerModel>.Fail("Müşteri verileri alınırken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<ResponseViewModel<UpdateCustomerModel>> UpdateCustomerAsync(UpdateCustomerModel updateCustomerModel)
        {
            var client = GetHttpClient();
            var response = await client.PutAsJsonAsync("customer/updateCustomer", updateCustomerModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<UpdateCustomerModel>.Fail("Müşteri güncelleme işlemi başarısız oldu. Lütfen tekrar deneyin.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<UpdateCustomerModel>>(responseBody);

            if (result == null)
            {
                return ResponseViewModel<UpdateCustomerModel>.Fail("Müşteri güncellenirken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return result;
        }
    }
}
