using StockTracker.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public class CustomerPayment: BaseEntity
    {

        public int CustomerAccountId { get; set; }

        [ForeignKey("CustomerAccountId")]
        public CustomerAccount CustomerAccount { get; set; }

        public decimal Amount { get; set; }  
        public string PaymentMethod { get; set; } 
        public DateTime PaymentDate { get; set; } 
        public string ReceivedBy { get; set; } 
    }
}
