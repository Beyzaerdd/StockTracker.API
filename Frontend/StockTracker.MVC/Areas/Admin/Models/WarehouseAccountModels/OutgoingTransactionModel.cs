using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.WarehouseAccountModels
{
    public class OutgoingTransactionModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("transactiondate")]
        public DateTime TransactionDate { get; set; }
    }
}
