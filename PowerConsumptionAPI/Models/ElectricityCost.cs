﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models
{
    public class ElectricityCost
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        // TODO: round to 2 decimal places

        [Precision(10, 2)]
        [Range(0, int.MaxValue, ErrorMessage = "Price field is required and must be a positive number.")]
        public decimal Price { get; set; }
    }
}
