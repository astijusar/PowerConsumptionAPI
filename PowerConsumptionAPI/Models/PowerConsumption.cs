using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models
{
    public class PowerConsumption
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public float CpuPowerDraw { get; set; }
        public float GpuPowerDraw { get; set; }

        public string ComputerId { get; set; }
        public Computer Computer { get; set; }
    }
}
