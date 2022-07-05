﻿using System.ComponentModel.DataAnnotations;
using static Parks.API.Models.Trail;

namespace Parks.API.Models.DTOs
{
    public class TrailCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Distance { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        //Reference
        [Required]
        public int NationalParkId { get; set; }
    }
}
