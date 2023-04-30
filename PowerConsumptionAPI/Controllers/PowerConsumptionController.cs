using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.ModelBinders;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.DTOs.Limit;
using PowerConsumptionAPI.Models.DTOs.PowerConsumption;
using PowerConsumptionAPI.Models.RequestFeatures;
using PowerConsumptionAPI.Repository;

namespace PowerConsumptionAPI.Controllers
{
    [Route("api/computer/{computerId}/power_consumption")]
    [ApiController]
    public class PowerConsumptionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<PowerConsumptionController> _logger;
        private readonly IMapper _mapper;

        public PowerConsumptionController(IRepositoryManager repository, ILogger<PowerConsumptionController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("/api/computer/power_consumption")]
        [ServiceFilter(typeof(ValidatePowerConsumptionParametersAttribute))]
        public async Task<IActionResult> GetPowerConsumptionData([FromQuery] PowerConsumptionParameters parameters)
        {
            var powerConsumptions = await _repository.PowerConsumption.GetPowerConsumptionsAsync(parameters, false);

            var powerConsunptionsDto = _mapper.Map<IEnumerable<PowerConsumptionDto>>(powerConsumptions);

            return Ok(powerConsunptionsDto);
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        [ServiceFilter(typeof(ValidatePowerConsumptionParametersAttribute))]
        public async Task<IActionResult> GetPowerConsumptionDataForComputer(string computerId, [FromQuery] PowerConsumptionParameters parameters)
        {
            var powerConsumptions = await _repository.PowerConsumption.GetPowerConsumptionsAsync(parameters, false, computerId);

            var powerConsunptionsDto = _mapper.Map<IEnumerable<PowerConsumptionDto>>(powerConsumptions);

            return Ok(powerConsunptionsDto);
        }

        [HttpGet("/api/power_consumption/limit")]
        public IActionResult GetLimits()
        {
            var limits = _repository.Limit.GetAllLimits(LimitType.Power, false);

            var limitsDto = _mapper.Map<IEnumerable<LimitDto>>(limits);

            return Ok(limitsDto);
        }

        [HttpPost("/api/power_consumption/limit")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateLimit([FromBody] LimitCreationDto limitDto)
        {
            var limit = _mapper.Map<Limit>(limitDto);
            limit.LimitType = LimitType.Power;

            _repository.Limit.CreateLimit(limit);
            _repository.Save();

            return Ok();
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> SavePowerConsumptionData(string computerId, [FromBody] IEnumerable<PowerConsumptionCreationDto> input)
        {
            var computer = HttpContext.Items["computer"] as Computer;

            var powerConsumption = _mapper.Map<IEnumerable<PowerConsumption>>(input);
            powerConsumption.ToList().ForEach(p => p.ComputerId = computerId);

            computer.Inactivity = powerConsumption.MaxBy(p => p.Time).Inactivity;

            _repository.PowerConsumption.CreatePowerConsumptions(powerConsumption);
            await _repository.SaveAsync();

            return Ok();
        }

        [HttpPut]
        [HttpPut("/api/power_consumption/limit/{limitId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateLimit(Guid limitId, LimitUpdateDto input)
        {
            var limit = _repository.Limit.GetLimitById(limitId, LimitType.Power, true);

            if (limit == null)
            {
                _logger.LogWarning($"Limit with id: {limitId} does not exist in the database");
                return NotFound();
            }

            _mapper.Map(input, limit);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("collection/({powerConsumptionIds})")]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> DeletePowerConsumptionData(string computerId,
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> powerConsumptionIds)
        {
            var powerConsumptions = await _repository.PowerConsumption.GetPowerConsumptionsByIdsAsync(computerId, powerConsumptionIds, false);

            if (!powerConsumptions.Any())
            {
                _logger.LogWarning($"There is no power consumption data with ids: {powerConsumptionIds}");
                return NotFound();
            }

            _repository.PowerConsumption.DeletePowerConsumptions(powerConsumptions);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("/api/power_consumption/limit")]
        public IActionResult DeleteLimit(Guid limitId)
        {
            var limit = _repository.Limit.GetLimitById(limitId, LimitType.Power, true);

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
