using StockTracker.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Entity.Concrete
{
    public  class CustomerAccount : BaseEntity
    {
  
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public decimal TotalAmount { get; set; } 

        public virtual ICollection<CustomerPayment> Payments { get; set; } = new List<CustomerPayment>();

        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount => TotalAmount - PaidAmount; 

        public string Description { get; set; }
        public int RentalId { get; set; }

        [ForeignKey("RentalId")]
        public Rental Rental { get; set; }
    }
}
