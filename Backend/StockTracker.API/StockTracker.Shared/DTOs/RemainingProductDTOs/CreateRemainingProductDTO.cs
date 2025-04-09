using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.RemainingItemDTOs
{
    public class CreateRemainingProductDTO
    {
        public int RentalItemId { get; set; }
        public int DaysRemaining { get; set; }
        public int QuantityRemaining { get; set; }
    }
}
