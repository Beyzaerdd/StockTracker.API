using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.RemainingProductDTO
{
    public class UpdateRemainingProductDTO
    {
        public int Id { get; set; }
        public int DaysRemaining { get; set; }
    }
}
