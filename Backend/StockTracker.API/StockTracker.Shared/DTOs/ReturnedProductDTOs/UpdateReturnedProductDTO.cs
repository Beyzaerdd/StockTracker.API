using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.ReturnedProductDTOs
{
    public class UpdateReturnedProductDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityReturned { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
