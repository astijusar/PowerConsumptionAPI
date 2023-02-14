namespace PowerConsumptionAPI.Models.DTOs.PowerConsumption
{
    public class PowerConsumptionDto
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public float CpuPowerDraw { get; set; }
        public float GpuPowerDraw { get; set; }
    }
}
