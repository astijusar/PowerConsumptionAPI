using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;

namespace PowerConsumptionAPI.Services
{
    public class CostCalculator : ICostCalculator
    {
        private readonly RepositoryContext _context;

        public CostCalculator(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<float> Calculate(ElectricityPriceParameters parameters)
        {
            var electricityCosts = _context.ElectricityCosts.ToList();

            if (electricityCosts == null)
            {
                return 0;
            }

            if (parameters.ComputerId != null)
            {
                var powerConsumptions = await _context.PowerConsumptions
                    .AsNoTracking()
                    .Where(p => p.ComputerId == parameters.ComputerId && p.Time >= parameters.From && p.Time <= parameters.To)
                    .ToListAsync();

                if (powerConsumptions == null)
                {
                    return 0;
                }

                return getCost(electricityCosts, powerConsumptions);
            }
            else
            {
                var powerConsumptions = await _context.PowerConsumptions
                    .AsNoTracking()
                    .Where(p => p.Time >= parameters.From && p.Time <= parameters.To)
                    .ToListAsync();

                if (powerConsumptions == null)
                {
                    return 0;
                }

                return getCost(electricityCosts, powerConsumptions);
            }
        }

        private float getCost(List<ElectricityCost> ec, List<PowerConsumption> pc)
        {
            float cost = 0;

            if (ec.Count() == 1)
            {
                foreach (var power in pc)
                {
                    cost += (power.TotalPowerDraw / 100000000) * (float)ec[0].Price;
                }
            }
            else
            {
                var firstInterval = new Tuple<TimeOnly, TimeOnly>(
                    ParseTimeOnly(ec[0].From),
                    ParseTimeOnly(ec[0].To));

                foreach (var power in pc)
                {
                    TimeOnly time = TimeOnly.FromDateTime(power.Time);

                    if (time.CompareTo(firstInterval.Item1) >= 0
                        && time.CompareTo(firstInterval.Item2) <= 0)
                    {
                        cost += (power.TotalPowerDraw / 100000000) * (float)ec[0].Price;
                    }
                    else
                    {
                        cost += (power.TotalPowerDraw / 100000000) * (float)ec[1].Price;
                    }
                }
            }

            return cost;
        }

        private TimeOnly ParseTimeOnly(string timeString)
        {
            if (TimeOnly.TryParse(timeString, out var time))
            {
                return time;
            }
            else
            {
                throw new ArgumentException($"Invalid time string: {timeString}");
            }
        }
    }
}
