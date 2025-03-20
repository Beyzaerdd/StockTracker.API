using StockTracker.Shared.DTOs.RemainingItemDTOs;
using StockTracker.Shared.DTOs.RemainingProductDTO;
using StockTracker.Shared.DTOs.RemainingProductDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public  interface IRemainingProductService
    {
        Task<ResponseDTO<IEnumerable<RemainingProductDTO>>> GetAllRemainingProductsAsync();
        Task<ResponseDTO<string>> UpdateRemainingProductAsync(UpdateRemainingProductDTO updateRemainingProductDTO);
        Task<ResponseDTO<string>> ProcessRemainingProductsAsync(int rentalId, bool createNewRental);
        Task<ResponseDTO<IEnumerable<RemainingProductDTO>>> GetRemainingProductsByRentalIdAsync(int rentalId);
    }
}
