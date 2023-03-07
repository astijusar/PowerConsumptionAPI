using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.ModelBinders;
using PowerConsumptionAPI.Models;
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

        [HttpGet]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> GetPowerConsumptionData(string computerId, [FromQuery] PowerConsumptionParameters parameters)
        {
            var powerConsumption = await _repository.PowerConsumption.GetPowerConsumptionsAsync(computerId, parameters, false);

            DateTime? nextCursor = powerConsumption.Any()
                ? powerConsumption.LastOrDefault().Time : null;

            var hasNextPage = nextCursor != null
                ? await _repository.PowerConsumption.AnyAsync(p => p.ComputerId == computerId && p.Time < nextCursor) : false;

            var powerConsumptionDto = _mapper.Map<IEnumerable<PowerConsumptionDto>>(powerConsumption);

            return Ok(new { data = powerConsumptionDto, 
                pagination = new Metadata { CurrentCursor = parameters.Cursor, NextCursor = hasNextPage ? nextCursor : null} });
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
    }
}
