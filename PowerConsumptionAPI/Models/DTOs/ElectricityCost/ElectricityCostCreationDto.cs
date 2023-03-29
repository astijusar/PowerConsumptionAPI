using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models.DTOs.ElectricityCost
{
    public class ElectricityCostCreationDto
    {
        [Required]
        public TimeOnly From { get; set; }

        [Required]
        public TimeOnly To { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price field is required and must be a positive number.")]
        public decimal Price { get; set; }
    }
}
