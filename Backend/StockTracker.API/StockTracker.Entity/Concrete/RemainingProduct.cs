using StockTracker.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public class RemainingProduct: BaseEntity
    {
    
        public int RentalItemId { get; set; }
        public RentalItem RentalItem { get; set; }
        public int DaysRemaining { get; set; }   // 30 gün kontrolü için
        public decimal DailyPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int QuantityRemaining { get; set; }
    }
}
