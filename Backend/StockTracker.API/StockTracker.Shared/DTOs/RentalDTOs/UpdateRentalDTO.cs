using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracker.Shared.DTOs.RentalItemDTOs;

namespace StockTracker.Shared.DTOs.RentalDTOs
{
    public class UpdateRentalDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal? VATRate { get; set; }
        public List<UpdateRentalItemDTO> RentalItems { get; set; } = new List<UpdateRentalItemDTO>();

      
    }
}
