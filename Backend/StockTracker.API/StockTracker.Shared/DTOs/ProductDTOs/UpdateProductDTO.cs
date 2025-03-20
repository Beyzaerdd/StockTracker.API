
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
     
        public string Name { get; set; }
        public decimal MonthlyPrice { get; set; }  // Aylık kiralama ücreti
        public int StockQuantity { get; set; }     // Depodaki mevcut stok

        public string Description { get; set; }
    }
}
