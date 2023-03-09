using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;
using PowerConsumptionAPI.Repository.Extensions;
using System.Linq.Expressions;

namespace PowerConsumptionAPI.Repository
{
    public class PowerConsumptionRepository : RepositoryBase<PowerConsumption>, IPowerConsumptionRepository
    {
        public PowerConsumptionRepository(RepositoryContext _repositoryContext)
            : base(_repositoryContext)
        {
            
        }

        public async Task<bool> AnyAsync(string computerId, PowerConsumptionParameters param) =>
            await _repositoryContext.PowerConsumptions.Where(p => p.ComputerId == computerId && p.Time < param.Cursor).FilterPowerConsumptions(param).AnyAsync();

        public void CreatePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions) =>
            CreateRange(powerConsumptions);

        public void DeletePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions) =>
            DeleteRange(powerConsumptions);

        public async Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsAsync(string computerId, PowerConsumptionParameters param, bool trackChanges) =>
            await FindByCondition(p => p.ComputerId == computerId && p.Time < param.Cursor, trackChanges)
            .FilterPowerConsumptions(param)
            .Sort(param.OrderBy)
            .Take(param.Count)
            .ToListAsync();

        public async Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsByIdsAsync(string computerId, IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(p => p.ComputerId == computerId && ids.Contains(p.Id), trackChanges)
            .ToListAsync();
    }
}
