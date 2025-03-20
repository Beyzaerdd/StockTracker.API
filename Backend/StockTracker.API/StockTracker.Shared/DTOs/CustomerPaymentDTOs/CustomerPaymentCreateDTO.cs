using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.CustomerPaymentDTOs
{
    public class CustomerPaymentCreateDTO
    {
        public int CustomerAccountId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string ReceivedBy { get; set; }
    }
}
