using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracker.Shared.DTOs.RentalItemDTOs;

namespace StockTracker.Shared.DTOs.RentalDTOs
{
    public class CreateRentalDTO
    {
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<CreateRentalItemDTO> RentalItems { get; set; }
         public decimal? VATRate { get; set; }  
    }
}
