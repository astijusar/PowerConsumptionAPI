namespace PowerConsumptionAPI.Models.DTOs.ElectricityCost
{
    public class ElectricityCostDto
    {
        public Guid Id { get; set; }
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }
        public decimal Price { get; set; }
    }
}
