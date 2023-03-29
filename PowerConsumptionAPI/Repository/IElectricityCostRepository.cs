using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Repository
{
    public interface IElectricityCostRepository
    {
        IEnumerable<ElectricityCost> GetAllElectricityCosts(bool trackChanges);
        IEnumerable<ElectricityCost> GetElectricityCostsById(IEnumerable<Guid> ids, bool trackChanges);
        void CreateElectricityCosts(IEnumerable<ElectricityCost> electricityCost);
        void DeleteElectricityCosts(IEnumerable<ElectricityCost> electricityCost);
    }
}
