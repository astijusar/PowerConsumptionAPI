using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace PowerConsumptionAPI.Repository.Extensions
{
    public static class ComputerRepositoryExtensions
    {
        public static IQueryable<Computer> Sort(this IQueryable<Computer> computers, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return computers.OrderBy(c => c.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Computer>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return computers.OrderBy(c => c.Name);

            return computers.OrderBy(orderQuery);
        }
    }
}
