using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.ReturnedProductModels
{
    public class CreateReturnedProductModel
    {
        [JsonPropertyName("rentalitemid")]
        public int RentalItemId { get; set; }

        [JsonPropertyName("quantityreturned")]
        public int QuantityReturned { get; set; }

        [JsonPropertyName("returndate")]
        public DateTime ReturnDate { get; set; }
    }

}
