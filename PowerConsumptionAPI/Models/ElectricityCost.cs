using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models
{
    public class ElectricityCost
    {
        [Key]
        public Guid Id { get; set; }


        [Required]
        public TimeOnly From { get; set; }


        [Required]
        public TimeOnly To { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Price field is required and must be a positive number.")]
        public decimal Price { get; set; }
    }
}
