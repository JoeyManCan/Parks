using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Parks.API.Models.DTOs
{
    public class NationalParkDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string State { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        [JsonPropertyName("Picture")]
        public string PictureUri { get; set; } = null!;
        public DateTime Established { get; set; }
    }
}
