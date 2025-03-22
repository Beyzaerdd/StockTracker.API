using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.CustomerPaymentModels
{
    public class CustomerPaymentCreateModel
    {
        [JsonPropertyName("customeraccountid")]
        public int CustomerAccountId { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("paymentmethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("paymentdate")]
        public DateTime PaymentDate { get; set; } = DateTime.Today;

        [JsonPropertyName("receivedby")]
        public string ReceivedBy { get; set; }
    }
}
