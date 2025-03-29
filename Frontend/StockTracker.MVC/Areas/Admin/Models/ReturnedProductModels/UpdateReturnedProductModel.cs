using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.ReturnedProductModels
{
    public class UpdateReturnedProductModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("productid")]
        public int ProductId { get; set; }

        [JsonPropertyName("productname")]
        public string ProductName { get; set; }

        [JsonPropertyName("quantityreturned")]
        public int QuantityReturned { get; set; }

        [JsonPropertyName("returndate")]
        public DateTime ReturnDate { get; set; }
    }
}
