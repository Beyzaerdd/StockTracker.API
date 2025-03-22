using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.ProductModels
{
    public class ProductStockInfoModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("productname")]
        public string ProductName { get; set; }

        [JsonPropertyName("stockquantity")]
        public int StockQuantity { get; set; }

        [JsonPropertyName("rentedquantity")]
        public int RentedQuantity { get; set; }

        [JsonPropertyName("remainingquantity")]
        public int RemainingQuantity { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
