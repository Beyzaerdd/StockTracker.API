using StockTracker.Shared.DTOs.ProductDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public interface IProductService
    {
        Task<ResponseDTO<ProductDTO>> GetProductByIdAsync(int id);
        Task<ResponseDTO<IEnumerable<ProductDTO>>> GetAllProductsAsync(int? take = null);
        Task<ResponseDTO<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO);
        Task<ResponseDTO<ProductDTO>> UpdateProductAsync(UpdateProductDTO updateProductDTO);
        Task<ResponseDTO<string>> DeleteProductAsync(int id);
        Task<ResponseDTO<List<ProductStockInfoDTO>>> GetProductStockInfoAsync();
    }
}
