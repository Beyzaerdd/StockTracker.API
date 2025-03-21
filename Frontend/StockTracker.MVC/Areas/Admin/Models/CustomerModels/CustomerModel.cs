using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.CustomerModels
{
    public class CustomerModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("lastname")]
        [Required]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        [Required]
        public string Phone { get; set; }

        [JsonPropertyName("address")]
        [Required]
        public string Address { get; set; }
    }
}
