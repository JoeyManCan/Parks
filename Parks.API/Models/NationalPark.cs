using System.ComponentModel.DataAnnotations;

namespace Parks.API.Models
{
    public class NationalPark
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public byte[] Picture { get; set; } = null!;
        public DateTime Established { get; set; }
    }
}
