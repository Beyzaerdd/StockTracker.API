using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public class OutgoingTransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; } 
        public string Description { get; set; } 
        public DateTime TransactionDate { get; set; } 
    }
}
