using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.RentalItemModels
{
    public class RentalItemModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("productid")]
        public int ProductId { get; set; }

        [JsonPropertyName("productname")]
        public string ProductName { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("monthlyprice")]
        public decimal MonthlyPrice { get; set; }

        [JsonPropertyName("totalprice")]
        public decimal TotalPrice => MonthlyPrice * Quantity;
    }
}
