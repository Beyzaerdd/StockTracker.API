using StockTracker.Shared.DTOs.AccountTransactionDTOs;
using StockTracker.Shared.DTOs.DeliveredItemDTOs;

using StockTracker.Shared.DTOs.RemainingItemDTOs;
using StockTracker.Shared.DTOs.RentalDTOs;
using StockTracker.Shared.DTOs.RentalItemDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
  public  interface IRentalService
    {
        Task<ResponseDTO<string>> CreateRentalAsync(CreateRentalDTO createRentalDTO);
        Task<ResponseDTO<string>> UpdateRentalAsync(int rentalId, UpdateRentalDTO updateRentalDTO);
        Task<ResponseDTO<string>> DeleteRentalAsync(int rentalId);
        Task<ResponseDTO<string>> UpdateRentalItemAsync(int rentalId, int rentalItemId, UpdateRentalItemDTO updateRentalItemDTO);
        Task<ResponseDTO<string>> DeleteRentalItemAsync(int rentalId, int rentalItemId);
        Task<ResponseDTO<List<RentalDTO>>> GetAllRentalsAsync();  // Yeni metod
        Task<ResponseDTO<RentalDTO>> GetRentalByIdAsync(int rentalId);

    }
}
