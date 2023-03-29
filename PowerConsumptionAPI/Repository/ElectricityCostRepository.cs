using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Repository
{
    public class ElectricityCostRepository : RepositoryBase<ElectricityCost>, IElectricityCostRepository
    {
        public ElectricityCostRepository(RepositoryContext _repositoryContext)
            :base(_repositoryContext)
        {

        }

        public void CreateElectricityCosts(IEnumerable<ElectricityCost> electricityCost) => CreateRange(electricityCost);

        public void DeleteElectricityCosts(IEnumerable<ElectricityCost> electricityCost) => DeleteRange(electricityCost);

        public IEnumerable<ElectricityCost> GetAllElectricityCosts(bool trackChanges) =>
            FindAll(trackChanges)
            .ToList();

        public IEnumerable<ElectricityCost> GetElectricityCostsById(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(e => ids.Contains(e.Id), trackChanges)
            .ToList();
    }
}
