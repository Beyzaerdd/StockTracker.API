﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs.CustomerDTOs
{
    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }



        public string Phone { get; set; }
        public string Address { get; set; }
    }
}