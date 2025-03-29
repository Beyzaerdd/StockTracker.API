using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models
{
    public class AdminNotificationModel


    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("isread")]
        public bool IsRead { get; set; } = false;
    }
}
