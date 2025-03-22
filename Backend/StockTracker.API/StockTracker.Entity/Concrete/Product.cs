using StockTracker.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public class Product : BaseEntity
    {


        public string Name { get; set; }
        public decimal MonthlyPrice { get; set; }  
        public int StockQuantity { get; set; }     

        public int Quantity { get; set; }

        public string Description { get; set; }
        public ICollection<RentalItem> RentalItems { get; set; }

    }
}
