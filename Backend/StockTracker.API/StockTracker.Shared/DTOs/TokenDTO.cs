using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.DTOs
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }//JWT
        public DateTime ExpirationDate { get; set; }
    }
}
