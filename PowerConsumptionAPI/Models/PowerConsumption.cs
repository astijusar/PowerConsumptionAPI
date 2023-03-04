using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerConsumptionAPI.Models
{
    public class PowerConsumption
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Time { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Inactivity field is required and must be a positive number.")]
        public int Inactivity { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "CpuPowerDraw field is required and must be a positive number.")]
        public float CpuPowerDraw { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "GpuPowerDraw field is required and must be a positive number.")]
        public float GpuPowerDraw { get; set; }

        public float TotalPowerDraw => CpuPowerDraw + GpuPowerDraw;

        [Required]
        [ForeignKey(nameof(Computer))]
        public string ComputerId { get; set; }
        public Computer Computer { get; set; }
    }
}
