using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.MVC.Areas.Admin.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }//JWT
        public DateTime ExpirationDate { get; set; }
    }
}
