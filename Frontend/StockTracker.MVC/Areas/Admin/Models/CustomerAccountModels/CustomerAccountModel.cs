using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.CustomerAccountModels
{
    public class CustomerAccountModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("customerid")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customername")]
        public string CustomerName { get; set; }

        [JsonPropertyName("startdate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("enddate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("totalamount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("paidamount")]
        public decimal PaidAmount { get; set; }

        [JsonPropertyName("remainingamount")]
        public decimal RemainingAmount => TotalAmount - PaidAmount;

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
