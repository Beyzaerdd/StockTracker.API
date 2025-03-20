using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.EmployeeDTOs
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }


        public string Phone { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
