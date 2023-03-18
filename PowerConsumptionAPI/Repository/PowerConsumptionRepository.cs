using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;
using PowerConsumptionAPI.Repository.Extensions;
using System.Linq;

namespace PowerConsumptionAPI.Repository
{
    public class PowerConsumptionRepository : RepositoryBase<PowerConsumption>, IPowerConsumptionRepository
    {
        public PowerConsumptionRepository(RepositoryContext _repositoryContext)
            : base(_repositoryContext)
        {
            
        }

        public void CreatePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions)
        {
            powerConsumptions.ToList().ForEach(p => p.TotalPowerDraw = p.CpuPowerDraw + p.GpuPowerDraw);

            CreateRange(powerConsumptions);
        }

        public void DeletePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions) =>
            DeleteRange(powerConsumptions);

        public async Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsAsync(string computerId, PowerConsumptionParameters param, bool trackChanges)
        {
            if (param.GroupBy == null)
            {
                return await FindByCondition(p => p.ComputerId == computerId, trackChanges)
                .FilterPowerConsumptions(param)
                .Sort(param.OrderBy)
                .Skip(param.PrevCount)
                .Take(param.Count)
                .ToListAsync();
            }

            return await FindByCondition(p => p.ComputerId == computerId, trackChanges)
                .FilterPowerConsumptions(param)
                .GroupBy(param.GroupBy)
                .Skip(param.PrevCount)
                .Take(param.Count)
                .ToListAsync();
        }

        public async Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsByIdsAsync(string computerId, IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(p => p.ComputerId == computerId && ids.Contains(p.Id), trackChanges)
            .ToListAsync();
    }
}
