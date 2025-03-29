using StockTracker.MVC.Areas.Admin.Models.RentalItemModels;

using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.RentalModels
{
    public class CreateRentalModel
    {
        [JsonPropertyName("customerid")]
        public int CustomerId { get; set; }

        [JsonPropertyName("startdate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("enddate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("rentalitems")]
        public List<CreateRentalItemModel> RentalItems { get; set; }

        private decimal? _vatRate = 0.20m;  // Varsayılan değeri belirle

        [JsonPropertyName("vatrate")]
        public decimal? VATRate
        {
            get => _vatRate;
            set => _vatRate = value ?? 0.20m;
        }
        [JsonPropertyName("totalprice")]
        public decimal TotalPrice { get; set; }
    }
}
