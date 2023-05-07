namespace PowerConsumptionAPI.Models.DTOs.ElectricityCost
{
    public class ElectricityCostDto
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Price { get; set; }
    }
}
