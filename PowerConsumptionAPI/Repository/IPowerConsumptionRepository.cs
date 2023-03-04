using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;
using System.Linq.Expressions;

namespace PowerConsumptionAPI.Repository
{
    public interface IPowerConsumptionRepository
    {
        Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsAsync(string computerId, PowerConsumptionParameters parameters, bool trackChanges);
        Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsByIdsAsync(string computerId, IEnumerable<Guid> ids, bool trackChanges);
        void CreatePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions);
        void DeletePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions);
        Task<bool> AnyAsync(Expression<Func<PowerConsumption, bool>> expression);
    }
}
