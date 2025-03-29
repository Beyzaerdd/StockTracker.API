using StockTracker.MVC.Areas.Admin.Models.RentalItemModels;
using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.RentalModels
{
    public class RentalModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("customerid")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customername")]
        public string CustomerName { get; set; }
        [JsonPropertyName("customerlastname")]
        public string CustomerLastName { get; set; }

        [JsonPropertyName("startdate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("enddate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("totalprice")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("rentalitems")]
        public List<RentalItemModel> RentalItems { get; set; }
        [JsonPropertyName("vatrate")]
        public decimal? VATRate { get; set; }
    }
}
