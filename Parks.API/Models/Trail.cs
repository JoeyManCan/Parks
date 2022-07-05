using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parks.API.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Distance { get; set; }
        public enum DifficultyLevel { Easy, Moderate, Difficulty, Expert }
        public DifficultyLevel Difficulty { get; set; }
        public DateTime DateCreated { get; set; }

        //Reference
        [Required]
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; } = null!;
    }
}
