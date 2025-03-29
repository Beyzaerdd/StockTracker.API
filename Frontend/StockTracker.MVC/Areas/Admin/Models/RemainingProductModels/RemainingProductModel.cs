using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.RemainingProductModels
{
    public class RemainingProductModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("productid")]
        public int ProductId { get; set; }

        [JsonPropertyName("productname")]
        public string ProductName { get; set; }

        [JsonPropertyName("daysremaining")]
        public int DaysRemaining { get; set; }

        [JsonPropertyName("dailyprice")]
        public decimal DailyPrice { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("totalprice")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("quantityremaining")]
        public int QuantityRemaining { get; set; }
    }
}
