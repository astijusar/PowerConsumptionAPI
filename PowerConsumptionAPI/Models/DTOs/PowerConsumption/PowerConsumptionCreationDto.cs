namespace PowerConsumptionAPI.Models.DTOs.PowerConsumption
{
    public class PowerConsumptionCreationDto
    {
        public DateTime Time { get; set; }
        public float CpuPowerDraw { get; set; }
        public float GpuPowerDraw { get; set; }
    }
}
