using StockTracker.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public class Customer: BaseEntity
    {
       
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }



        public string Phone { get; set; }
        public string Address { get; set; }


        public ICollection<Rental> Rentals { get; set; }
        public ICollection<CustomerAccount> Transactions { get; set; }
    }
}
