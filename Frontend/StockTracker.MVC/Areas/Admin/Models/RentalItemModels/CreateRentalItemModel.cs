using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.RentalItemModels
{
    public class CreateRentalItemModel
    {
        [JsonPropertyName("productid")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("monthlyprice")]
        public decimal MonthlyPrice { get; set; }
    }
}
