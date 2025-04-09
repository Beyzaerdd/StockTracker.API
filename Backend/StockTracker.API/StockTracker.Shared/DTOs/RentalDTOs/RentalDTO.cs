using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracker.Shared.DTOs.RentalItemDTOs;

namespace StockTracker.Shared.DTOs.RentalDTOs
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string CustomerLastName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? VATRate { get; set; }
        public decimal? Shipping { get; set; }
        public List<RentalItemDTO> RentalItems { get; set; }
    }
}
