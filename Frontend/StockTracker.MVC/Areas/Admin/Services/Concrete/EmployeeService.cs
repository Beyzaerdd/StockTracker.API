using Newtonsoft.Json;
using StockTracker.MVC.Areas.Admin.Models.EmployeeModels;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Services.Abstract;

namespace StockTracker.MVC.Services.Concrete
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        { }

        public async Task<ResponseViewModel<CreateEmployeeModel>> CreateEmployeeAsync(CreateEmployeeModel createEmployeeModel)
        {
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync("employee/createEmployee", createEmployeeModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<CreateEmployeeModel>.Fail("Çalışan oluşturulamadı.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<CreateEmployeeModel>>(responseBody);
            return result ?? ResponseViewModel<CreateEmployeeModel>.Fail("Bilinmeyen bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<IEnumerable<EmployeeModel>>> GetAllEmployeesAsync()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("employee/allEmployees");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<IEnumerable<EmployeeModel>>.Fail("Çalışan listesi alınamadı.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<IEnumerable<EmployeeModel>>>(responseBody);
            return result ?? ResponseViewModel<IEnumerable<EmployeeModel>>.Fail("Bilinmeyen bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<EmployeeModel>> GetEmployeeByIdAsync(int id)
        {
            var client = GetHttpClient();
            var response = await client.GetAsync($"employee/getEmployeeById/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<EmployeeModel>.Fail("Çalışan bilgisi alınamadı.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<EmployeeModel>>(responseBody);
            return result ?? ResponseViewModel<EmployeeModel>.Fail("Bilinmeyen bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<UpdateEmployeeModel>> UpdateEmployeeAsync(UpdateEmployeeModel updateEmployeeModel)
        {
            var client = GetHttpClient();
            var response = await client.PutAsJsonAsync("employee/updateEmployee", updateEmployeeModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<UpdateEmployeeModel>.Fail("Çalışan güncellenemedi.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<UpdateEmployeeModel>>(responseBody);
            return result ?? ResponseViewModel<UpdateEmployeeModel>.Fail("Bilinmeyen bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<string>> DeleteEmployeeAsync(int id)
        {
            var client = GetHttpClient();
            var response = await client.DeleteAsync($"employee/deleteEmployee/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<string>.Fail("Çalışan silinemedi.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<string>>(responseBody);
            return result ?? ResponseViewModel<string>.Fail("Bilinmeyen bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }
    }
}

