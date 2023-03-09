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

        public async Task<bool> AnyAsync(string computerId, PowerConsumptionParameters param) =>
            await _repositoryContext.PowerConsumptions.AnyAsync(p => p.ComputerId == computerId && p.Time < param.Cursor &&
                 (p.Inactivity >= param.MinInactivity && p.Inactivity <= param.MaxInactivity &&
                 p.Time >= param.MinTime && p.Time <= param.MaxTime &&
                 (p.CpuPowerDraw + p.GpuPowerDraw) >= param.MinTotalDraw && (p.CpuPowerDraw + p.GpuPowerDraw) <= param.MaxTotalDraw &&
                 p.CpuPowerDraw >= param.MinCpuDraw && p.CpuPowerDraw <= param.MaxCpuDraw &&
                 p.GpuPowerDraw >= param.MinGpuDraw && p.GpuPowerDraw <= param.MaxGpuDraw));

        public void CreatePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions) =>
            CreateRange(powerConsumptions);

        public void DeletePowerConsumptions(IEnumerable<PowerConsumption> powerConsumptions) =>
            DeleteRange(powerConsumptions);

        public async Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsAsync(string computerId, PowerConsumptionParameters param, bool trackChanges) =>
            await FindByCondition(p => p.ComputerId == computerId && p.Time < param.Cursor &&
                (p.Inactivity >= param.MinInactivity && p.Inactivity <= param.MaxInactivity &&
                 p.Time >= param.MinTime && p.Time <= param.MaxTime &&
                 (p.CpuPowerDraw + p.GpuPowerDraw) >= param.MinTotalDraw && (p.CpuPowerDraw + p.GpuPowerDraw) <= param.MaxTotalDraw &&
                 p.CpuPowerDraw >= param.MinCpuDraw && p.CpuPowerDraw <= param.MaxCpuDraw &&
                 p.GpuPowerDraw >= param.MinGpuDraw && p.GpuPowerDraw <= param.MaxGpuDraw), trackChanges)
            .OrderByDescending(p => p.Time)
            .Take(param.Count)
            .ToListAsync();

        public async Task<IEnumerable<PowerConsumption>> GetPowerConsumptionsByIdsAsync(string computerId, IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(p => p.ComputerId == computerId && ids.Contains(p.Id), trackChanges)
            .ToListAsync();
    }
}
