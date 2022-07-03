using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Parks.API.Models.DTOs
{
    public class NationalParkUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        [JsonPropertyName("Picture")]
        public string PictureUri { get; set; } = string.Empty!;
        public DateTime Established { get; set; }
    }
}
