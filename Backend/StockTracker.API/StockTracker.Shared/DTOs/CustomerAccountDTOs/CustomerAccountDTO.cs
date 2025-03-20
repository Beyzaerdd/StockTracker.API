using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StockTracker.Shared.DTOs.AccountTransactionDTOs
{
    public class CustomerAccountDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount => TotalAmount - PaidAmount;
        public string Description { get; set; }
    }
}
