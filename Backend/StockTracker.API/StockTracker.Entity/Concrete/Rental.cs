using StockTracker.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public  class Rental: BaseEntity
    {
   
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<RentalItem> RentalItems { get; set; }
        public decimal? VATRate { get; set; }

        public decimal? Shipping { get; set; }


    }
}
