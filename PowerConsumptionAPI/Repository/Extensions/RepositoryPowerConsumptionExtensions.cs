using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;
using PowerConsumptionAPI.Repository.Extensions.Utility;
using System;
using System.Globalization;
using System.Linq;
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

        public static IQueryable<PowerConsumption> Sort(this IQueryable<PowerConsumption> powerConsumptions, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return powerConsumptions.OrderByDescending(p => p.Time);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<PowerConsumption>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return powerConsumptions.OrderByDescending(p => p.Time);

            return powerConsumptions.OrderBy(orderQuery);
        }

        public static IQueryable<PowerConsumption> GroupBy(this IQueryable<PowerConsumption> powerConsumptions, string groupBy)
        {
            switch (groupBy)
            {
                case "hour":
                    powerConsumptions = powerConsumptions.OrderByDescending(p => p.Time)
                        .GroupBy(p => new DateTime(p.Time.Year, p.Time.Month, p.Time.Day, p.Time.Hour, 0, 0))
                        .Select(g => new PowerConsumption
                        {
                            Time = g.Key,
                            Inactivity = g.Sum(p => p.Inactivity),
                            CpuPowerDraw = g.Sum(p => p.CpuPowerDraw),
                            GpuPowerDraw = g.Sum(p => p.GpuPowerDraw),
                            TotalPowerDraw = g.Sum(p => p.TotalPowerDraw)
                        });
                    break;
                case "day":
                    powerConsumptions = powerConsumptions.OrderByDescending(p => p.Time)
                        .GroupBy(p => new DateTime(p.Time.Year, p.Time.Month, p.Time.Day, 0, 0, 0))
                        .Select(g => new PowerConsumption
                        {
                            Time = g.Key,
                            Inactivity = g.Sum(p => p.Inactivity),
                            CpuPowerDraw = g.Sum(p => p.CpuPowerDraw),
                            GpuPowerDraw = g.Sum(p => p.GpuPowerDraw),
                            TotalPowerDraw = g.Sum(p => p.TotalPowerDraw)
                        });
                    break;
                case "month":
                    powerConsumptions = powerConsumptions.OrderByDescending(p => p.Time)
                        .GroupBy(p => new DateTime(p.Time.Year, p.Time.Month, 1, 0, 0, 0))
                        .Select(g => new PowerConsumption
                        {
                            Time = g.Key,
                            Inactivity = g.Sum(p => p.Inactivity),
                            CpuPowerDraw = g.Sum(p => p.CpuPowerDraw),
                            GpuPowerDraw = g.Sum(p => p.GpuPowerDraw),
                            TotalPowerDraw = g.Sum(p => p.TotalPowerDraw)
                        });
                    break;
                case "year":
                    powerConsumptions = powerConsumptions.OrderByDescending(p => p.Time)
                        .GroupBy(p => new DateTime(p.Time.Year, 1, 1, 0, 0, 0))
                        .Select(g => new PowerConsumption
                        {
                            Time = g.Key,
                            Inactivity = g.Sum(p => p.Inactivity),
                            CpuPowerDraw = g.Sum(p => p.CpuPowerDraw),
                            GpuPowerDraw = g.Sum(p => p.GpuPowerDraw),
                            TotalPowerDraw = g.Sum(p => p.TotalPowerDraw)
                        });
                    break;
                default:
                    break;
            }

            return powerConsumptions;
        }
    }
}
