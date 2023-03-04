using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models.DTOs.PowerConsumption
{
    public class PowerConsumptionCreationDto
    {
        public DateTime Time { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Inactivity field is required and must be a positive number.")]
        public int Inactivity { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "CpuPowerDraw field is required and must be a positive number.")]
        public float CpuPowerDraw { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "GpuPowerDraw field is required and must be a positive number.")]
        public float GpuPowerDraw { get; set; }
    }
}
