namespace PowerConsumptionAPI.Models.RequestFeatures
{
    public class ElectricityPriceParameters
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string ComputerId { get; set; }
    }
}
