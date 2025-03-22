using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.CustomerPaymentModels
{
    public class CustomerPaymentModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("customeraccountid")]
        public int CustomerAccountId { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("paymentmethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("paymentdate")]
        public DateTime PaymentDate { get; set; }

        [JsonPropertyName("receivedby")]
        public string ReceivedBy { get; set; }
    }
}
