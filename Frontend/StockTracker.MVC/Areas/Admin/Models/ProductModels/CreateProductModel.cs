using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.ProductModels
{
    public class CreateProductModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("monthlyprice")]
        public decimal MonthlyPrice { get; set; }  // Aylık kiralama ücreti

        [JsonPropertyName("stockquantity")]
        public int StockQuantity { get; set; }     // Depodaki mevcut stok

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
