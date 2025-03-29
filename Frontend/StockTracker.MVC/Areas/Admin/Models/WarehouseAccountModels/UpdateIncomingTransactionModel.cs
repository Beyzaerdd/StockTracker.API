using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.WarehouseAccountModels
{
    public class UpdateIncomingTransactionModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("amount")]
        [Required]
        public decimal Amount { get; set; }

        [JsonPropertyName("description")]
        [StringLength(500)]
        public string Description { get; set; }

        [JsonPropertyName("transactiondate")]
        public DateTime TransactionDate { get; set; }
    }
}
