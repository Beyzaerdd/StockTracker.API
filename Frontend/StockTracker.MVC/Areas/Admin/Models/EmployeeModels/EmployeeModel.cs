using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.EmployeeModels
{
    public class EmployeeModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("salary")]
        public decimal Salary { get; set; }

        [JsonPropertyName("startdate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("enddate")]
        public DateTime? EndDate { get; set; }
    }

}