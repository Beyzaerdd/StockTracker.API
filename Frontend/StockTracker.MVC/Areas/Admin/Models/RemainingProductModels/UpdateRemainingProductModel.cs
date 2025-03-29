using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.RemainingProductModels
{
    public class UpdateRemainingProductModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("daysremaining")]
        public int DaysRemaining { get; set; }

        [JsonPropertyName("quantityremaining")]
        public int QuantityRemaining { get; set; }
    }
}
