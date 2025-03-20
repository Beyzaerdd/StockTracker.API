using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StockTracker.Shared.DTOs.AccountTransactionDTOs
{
    public class CreateCustomerAccountDTO
    {
        public int CustomerId { get; set; }
        public decimal PaidAmount { get; set; }
        public string Description { get; set; }
    }
}
