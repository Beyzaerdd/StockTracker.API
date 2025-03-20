using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public class RentalItem
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]  // Explicit foreign key declaration
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal TotalPrice => Quantity * MonthlyPrice;
    }
}
