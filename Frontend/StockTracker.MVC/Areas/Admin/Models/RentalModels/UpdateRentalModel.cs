using StockTracker.MVC.Areas.Admin.Models.RentalItemModels;
using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.RentalModels
{
    public class UpdateRentalModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("startdate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("enddate")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("monthly_price")]
        public decimal MonthlyPrice { get; set; }

        [JsonPropertyName("vatrate")]
        public decimal? VATRate { get; set; }

        [JsonPropertyName("rentalitems")]
        public List<UpdateRentalItemModel> RentalItems { get; set; } = new List<UpdateRentalItemModel>();
    }
}
