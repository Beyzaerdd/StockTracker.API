using StockTracker.Shared.DTOs.RentalItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.DeliveredItemDTOs
{
    public class CreateReturnedProductDTO
    {

        public int RentalItemId { get; set; }
        public int QuantityReturned { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
