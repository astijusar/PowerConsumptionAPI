using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerConsumptionAPI.Models.DTOs.Limit;
using PowerConsumptionAPI.Models.DTOs.ElectricityCost;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Repository;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.ModelBinders;
using PowerConsumptionAPI.Models.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace PowerConsumptionAPI.Controllers
{
    [Route("api/electricity_cost")]
    [ApiController]
    public class ElectricityCostController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<ElectricityCostController> _logger;
        private readonly IMapper _mapper;
        private readonly RepositoryContext _context;

        public ElectricityCostController(IRepositoryManager repository, ILogger<ElectricityCostController> logger, IMapper mapper, RepositoryContext context)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetElectricityCosts()
        {
            var electricityCosts = _repository.ElectricityCost.GetAllElectricityCosts(false);

            var electricityCostsDto = _mapper.Map<IEnumerable<ElectricityCostDto>>(electricityCosts);

            return Ok(electricityCostsDto);
        }

        [HttpGet("price")]
        public async Task<IActionResult> GetElectricityPrice([FromQuery] ElectricityPriceParameters parameters)
        {
            float cost = 0;
            var electricityCosts = _repository.ElectricityCost.GetAllElectricityCosts(false);

            if (electricityCosts == null)
            {
                return Ok(cost);
            }

            if (parameters.ComputerId != null)
            {
                var powerConsumptions = await _context.PowerConsumptions
                    .AsNoTracking()
                    .Where(p => p.ComputerId == parameters.ComputerId && p.Time >= parameters.From && p.Time <= parameters.To)
                    .ToListAsync();

                if (powerConsumptions == null)
                {
                    return Ok(cost);
                }

                foreach(var power in powerConsumptions)
                {
                    cost += power.TotalPowerDraw * (float)electricityCosts.ElementAt(0).Price;
                }
            } 
            else
            {
                var powerConsumptions = await _context.PowerConsumptions
                    .AsNoTracking()
                    .Where(p => p.Time >= parameters.From && p.Time <= parameters.To)
                    .ToListAsync();

                if (powerConsumptions == null)
                {
                    return Ok(0);
                }

                foreach (var power in powerConsumptions)
                {
                    cost += power.TotalPowerDraw * (float)electricityCosts.ElementAt(0).Price;
                }
            }

            return Ok(cost);
        }

        [HttpGet("limit")]
        public IActionResult GetLimits()
        {
            var limits = _repository.Limit.GetAllLimits(LimitType.Cost, false);

            var limitsDto = _mapper.Map<IEnumerable<LimitDto>>(limits);

            return Ok(limitsDto);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateElectricityCosts([FromBody] IEnumerable<ElectricityCostCreationDto> electricityCostDto)
        {
            var electricityCosts = _mapper.Map<IEnumerable<ElectricityCost>>(electricityCostDto);

            _repository.ElectricityCost.CreateElectricityCosts(electricityCosts);
            _repository.Save();

            return Ok();
        }

        [HttpPut("collection/({electricityCostsIds})")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult UpdateElectricityCosts(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> electricityCostsIds,
            [FromBody] IEnumerable<ElectricityCostUpdateDto> input)
        {
            var electricityCosts = _repository.ElectricityCost.GetElectricityCostsById(electricityCostsIds, true);

            if (electricityCosts == null)
            {
                _logger.LogWarning($"Electricity costs with ids: {electricityCostsIds} does not exist in the database");
                return NotFound();
            }

            for(int i = 0; i < input.Count(); i++)
            {
                electricityCosts.ElementAt(i).From = input.ElementAt(i).From;
                electricityCosts.ElementAt(i).To = input.ElementAt(i).To;
                electricityCosts.ElementAt(i).Price = input.ElementAt(i).Price;
            }

            _repository.Save();

            return NoContent();
        }

        [HttpPost("limit")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateLimit([FromBody] LimitCreationDto limitDto)
        {
            var limit = _mapper.Map<Limit>(limitDto);
            limit.LimitType = LimitType.Cost;

            _repository.Limit.CreateLimit(limit);
            _repository.Save();

            return Ok();
        }

        [HttpDelete("collection/({electricityCostsIds})")]
        public IActionResult DeleteElectricityCosts(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> electricityCostsIds)
        {
            var electricityCosts = _repository.ElectricityCost.GetElectricityCostsById(electricityCostsIds, true);

            if (!electricityCosts.Any())
            {
                _logger.LogWarning($"There is no electricity cost data with ids: {electricityCostsIds}");
                return NotFound();
            }

            _repository.ElectricityCost.DeleteElectricityCosts(electricityCosts);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("limit")]
        public IActionResult DeleteLimit(Guid limitId)
        {
            var limit = _repository.Limit.GetLimitById(limitId, false, LimitType.Cost);

            if (limit == null)
            {
                _logger.LogWarning($"There is no limit with id: {limitId}");
                return NotFound();
            }

            _repository.Limit.DeleteLimit(limit);
            _repository.Save();

            return NoContent();
        }
    }
}
