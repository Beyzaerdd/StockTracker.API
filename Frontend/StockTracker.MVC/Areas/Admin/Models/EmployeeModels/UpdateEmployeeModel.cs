using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockTracker.MVC.Areas.Admin.Models.EmployeeModels
{
    public class UpdateEmployeeModel
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "İsim alanı gereklidir.")]
        public string Name { get; set; }

        [JsonPropertyName("lastname")]
        [Required(ErrorMessage = "Soyisim alanı gereklidir.")]
        public string LastName { get; set; }

        [JsonPropertyName("phone")]
        [Required(ErrorMessage = "Telefon alanı gereklidir.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; }

        [JsonPropertyName("position")]
        [Required(ErrorMessage = "Pozisyon alanı gereklidir.")]
        public string Position { get; set; }

        [JsonPropertyName("salary")]
        [Required(ErrorMessage = "Maaş alanı gereklidir.")]
        [Range(0, double.MaxValue, ErrorMessage = "Maaş negatif olamaz.")]
        public decimal Salary { get; set; }

        [JsonPropertyName("startdate")]
        [Required(ErrorMessage = "Başlangıç tarihi gereklidir.")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("enddate")]
        public DateTime? EndDate { get; set; }
    }
}
