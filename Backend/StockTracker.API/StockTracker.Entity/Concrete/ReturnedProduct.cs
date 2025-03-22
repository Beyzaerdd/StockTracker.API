using StockTracker.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public class ReturnedProduct : BaseEntity
    {

        public int RentalItemId { get; set; }
        public RentalItem RentalItem { get; set; }
        public int QuantityReturned { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
