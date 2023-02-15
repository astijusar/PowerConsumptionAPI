using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerConsumptionAPI.Models
{
    public class PowerConsumption
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public float CpuPowerDraw { get; set; }
        public float GpuPowerDraw { get; set; }

        [Required]
        [ForeignKey(nameof(Computer))]
        public string ComputerId { get; set; }
        public Computer Computer { get; set; }
    }
}
