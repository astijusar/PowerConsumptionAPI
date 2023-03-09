using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace PowerConsumptionAPI.Repository.Extensions
{
    public static class RepositoryPowerConsumptionExtensions
    {
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
