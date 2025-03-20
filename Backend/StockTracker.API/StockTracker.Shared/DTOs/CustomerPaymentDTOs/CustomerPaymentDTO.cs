using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.CustomerPaymentDTOs
{
    public class CustomerPaymentDTO
    {
        public int Id { get; set; }
        public int CustomerAccountId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ReceivedBy { get; set; }
    }
}
