using PowerConsumptionAPI.Models.RequestFeatures;

namespace PowerConsumptionAPI.Services
{
    public interface ICostCalculator
    {
        Task<float> Calculate(ElectricityPriceParameters parameters);
    }
}