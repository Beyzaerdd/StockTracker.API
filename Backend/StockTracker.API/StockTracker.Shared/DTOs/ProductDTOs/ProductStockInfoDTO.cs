using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.ProductDTOs
{
    public class ProductStockInfoDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; } 
        public int RentedQuantity { get; set; } 
        public int RemainingQuantity { get; set; }
        public int Quantity { get; set; }
    }
}
