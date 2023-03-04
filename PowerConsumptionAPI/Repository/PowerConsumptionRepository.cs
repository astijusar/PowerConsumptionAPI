using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;
using System.Linq.Expressions;

namespace PowerConsumptionAPI.Repository
{
    public class PowerConsumptionRepository : RepositoryBase<PowerConsumption>, IPowerConsumptionRepository
    {
        public PowerConsumptionRepository(RepositoryContext _repositoryContext)
            : base(_repositoryContext)
        {

        }

        public async Task<bool> AnyAsync(Expression<Func<PowerConsumption, bool>> expression) =>
            await _repositoryContext.PowerConsumptions.AnyAsync(expression);

        public void CreatePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions) =>
            CreateRange(powerConsumptions);

        public void DeletePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions) =>
            DeleteRange(powerConsumptions);

        public async Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsAsync(string computerId, PowerConsumptionParameters parameters, bool trackChanges) =>
            await FindByCondition(p => p.ComputerId == computerId && p.Time < parameters.Cursor, trackChanges)
            .OrderByDescending(p => p.Time)
            .Take(parameters.Count)
            .ToListAsync();

        public async Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsByIdsAsync(string computerId, IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(p => p.ComputerId == computerId && ids.Contains(p.Id), trackChanges)
            .ToListAsync();
    }
}
