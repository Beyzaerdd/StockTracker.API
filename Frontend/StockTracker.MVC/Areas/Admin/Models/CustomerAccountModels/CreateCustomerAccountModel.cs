using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.CustomerAccountModels
{
    public class CreateCustomerAccountModel
    {
        [JsonPropertyName("customerid")]
        public int CustomerId { get; set; }

        [JsonPropertyName("paidamount")]
        public decimal PaidAmount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
