using StockTracker.Shared.DTOs.RentalItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.RentalDTOs
{
    class AddNewProductsToRentalDTO
    {
        public int RentalId { get; set; }
        public List<CreateRentalItemDTO> RentalItems { get; set; }
    }
}
