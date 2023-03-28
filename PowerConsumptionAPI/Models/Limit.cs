using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models
{
    public class Limit
    {
        [Key]
        public Guid Id { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "MaxValue field is required and must be a positive number.")]
        public float MaxValue { get; set; }


        [Required]
        public LimitType LimitType { get; set; }
    }
}
