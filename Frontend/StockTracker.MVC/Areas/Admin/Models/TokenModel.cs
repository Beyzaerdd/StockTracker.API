using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockTracker.MVC.Areas.Admin.Models
{
    public class TokenModel
    {

        [JsonPropertyName("accesstoken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expirationdate")]
        public DateTime ExpirationDate { get; set; }
    }
}
