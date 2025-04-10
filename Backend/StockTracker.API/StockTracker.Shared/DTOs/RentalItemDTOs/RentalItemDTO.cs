﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.RentalItemDTOs
{
    public class RentalItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal TotalPrice => MonthlyPrice * Quantity;
    }
}
