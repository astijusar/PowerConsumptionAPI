using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;
using PowerConsumptionAPI.Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace PowerConsumptionAPI.Repository.Extensions
{
    public static class RepositoryPowerConsumptionExtensions
    {
        public static IQueryable<PowerConsumption> FilterPowerConsumptions(this IQueryable<PowerConsumption> powerConsumptions, PowerConsumptionParameters param) =>
            powerConsumptions.Where(p => p.Inactivity >= param.MinInactivity && p.Inactivity <= param.MaxInactivity &&
                 p.Time >= param.MinTime && p.Time <= param.MaxTime &&
                 p.TotalPowerDraw >= param.MinTotalDraw && p.TotalPowerDraw <= param.MaxTotalDraw &&
                 p.CpuPowerDraw >= param.MinCpuDraw && p.CpuPowerDraw <= param.MaxCpuDraw &&
                 p.GpuPowerDraw >= param.MinGpuDraw && p.GpuPowerDraw <= param.MaxGpuDraw);

        public static IQueryable<PowerConsumption> Sort(this IQueryable<PowerConsumption> powerConsumptions, string orderByQueryString)
        {
            if (string.IsNullOrEmpty(orderByQueryString))
                return powerConsumptions.OrderByDescending(p => p.Time);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<PowerConsumption>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return powerConsumptions.OrderByDescending(p => p.Time);

            return powerConsumptions.OrderBy(orderQuery);
        }
    }
}
