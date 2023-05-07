using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models.DTOs.ElectricityCost
{
    public abstract class ElectricityCostModificationDto
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price field is required and must be a positive number.")]
        public decimal Price { get; set; }
    }
}
