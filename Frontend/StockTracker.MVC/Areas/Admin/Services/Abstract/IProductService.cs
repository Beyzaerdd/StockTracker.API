using ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels;
using StockTracker.MVC.Areas.Admin.Models.ProductModels;

namespace StockTracker.MVC.Areas.Admin.Services.Abstract
{
    public interface IProductService
    {
        Task<ResponseViewModel<IEnumerable<ProductModel>>> GetAllProductsAsync(int count = 11);
        Task<ResponseViewModel<ProductModel>> GetProductByIdAsync(int id);
        Task<ResponseViewModel<CreateProductModel>> CreateProductAsync(CreateProductModel createProductModel);
        Task<ResponseViewModel<UpdateProductModel>> UpdateProductAsync(UpdateProductModel updateProductModel);
        Task<ResponseViewModel<ProductModel>> DeleteProductAsync(int id);
        Task<ResponseViewModel<List<ProductStockInfoModel>>> GetProductStockInfoAsync();
    }
}
