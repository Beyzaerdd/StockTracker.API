using StockTracker.Shared.DTOs.DeliveredItemDTOs;
using StockTracker.Shared.DTOs.RemainingItemDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using StockTracker.Shared.DTOs.ReturnedProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public interface IReturnedProductService
    {
        Task<ResponseDTO<CreateReturnedProductDTO>> CreateReturnedProductAsync(CreateReturnedProductDTO createReturnedProductDTO);
        Task<ResponseDTO<IEnumerable<ReturnedProductDTO>>> GetAllReturnedProductsAsync();
        Task<ResponseDTO<ReturnedProductDTO>> GetReturnedProductByIdAsync(int id);
        Task<ResponseDTO<string>> UpdateReturnedProductAsync(UpdateReturnedProductDTO updateReturnedProduct);
        Task<ResponseDTO<string>> DeleteReturnedProductAsync(int id);

    }
}
