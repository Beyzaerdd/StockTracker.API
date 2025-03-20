using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.RentalItemDTOs
{
    public class CreateRentalItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal MonthlyPrice { get; set; }
    }
}

