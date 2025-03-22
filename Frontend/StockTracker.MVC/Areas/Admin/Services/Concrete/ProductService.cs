using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using Newtonsoft.Json;
using StockTracker.MVC.Areas.Admin.Models.ProductModels;
using StockTracker.MVC.Areas.Admin.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StockTracker.MVC.Areas.Admin.Services.Concrete
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) :
            base(httpClientFactory, httpContextAccessor)
        { }

        public async Task<ResponseViewModel<IEnumerable<ProductModel>>> GetAllProductsAsync(int count=11)
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<IEnumerable<ProductModel>>>($"product/allProducts?take={count}");

            if (response == null || !response.success)
            {
                return ResponseViewModel<IEnumerable<ProductModel>>.Fail("Ürünler alınırken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<ResponseViewModel<ProductModel>> GetProductByIdAsync(int id)
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<ProductModel>>($"product/getProductById/{id}");

            if (response == null || !response.success)
            {
                return ResponseViewModel<ProductModel>.Fail("Ürün verileri alınırken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<ResponseViewModel<CreateProductModel>> CreateProductAsync(CreateProductModel createProductModel)
        {
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync("product/createProduct", createProductModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<CreateProductModel>.Fail("Ürün oluşturma işlemi başarısız oldu.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<CreateProductModel>>(responseBody);

            return result ?? ResponseViewModel<CreateProductModel>.Fail("Ürün oluşturulurken bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<UpdateProductModel>> UpdateProductAsync(UpdateProductModel updateProductModel)
        {
            var client = GetHttpClient();
            var response = await client.PutAsJsonAsync("product/updateProduct", updateProductModel);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<UpdateProductModel>.Fail("Ürün güncelleme işlemi başarısız oldu.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<UpdateProductModel>>(responseBody);

            return result ?? ResponseViewModel<UpdateProductModel>.Fail("Ürün güncellenirken bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<ProductModel>> DeleteProductAsync(int id)
        {
            var client = GetHttpClient();
            var response = await client.DeleteAsync($"product/deleteProduct/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(responseBody))
            {
                return ResponseViewModel<ProductModel>.Fail("Ürün silme işlemi başarısız oldu.", StatusCodes.Status400BadRequest);
            }

            var result = JsonConvert.DeserializeObject<ResponseViewModel<ProductModel>>(responseBody);

            return result ?? ResponseViewModel<ProductModel>.Fail("Ürün silinirken bir hata oluştu.", StatusCodes.Status500InternalServerError);
        }

        public async Task<ResponseViewModel<List<ProductStockInfoModel>>> GetProductStockInfoAsync()
        {
            var client = GetHttpClient();
            var response = await client.GetFromJsonAsync<ResponseViewModel<List<ProductStockInfoModel>>>("product/getProductStockInfo");

            if (response == null || !response.success)
            {
                return ResponseViewModel<List<ProductStockInfoModel>>.Fail("Ürün stok bilgileri alınırken bir hata oluştu.", StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}
