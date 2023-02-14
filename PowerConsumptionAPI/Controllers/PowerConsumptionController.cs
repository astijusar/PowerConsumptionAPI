using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.ModelBinders;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.DTOs.PowerConsumption;

namespace PowerConsumptionAPI.Controllers
{
    [Route("api/computer/{computerId}/power_consumption")]
    [ApiController]
    public class PowerConsumptionController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly ILogger<PowerConsumptionController> _logger;
        private readonly IMapper _mapper;

        public PowerConsumptionController(RepositoryContext repositoryContext, ILogger<PowerConsumptionController> logger, IMapper mapper)
        {
            _repositoryContext = repositoryContext;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> GetPowerConsumptionData(string computerId)
        {
            var powerConsumption = await _repositoryContext.PowerConsumptions
                .AsNoTracking()
                .ToListAsync();

            var powerConsumptionDto = _mapper.Map<IEnumerable<PowerConsumptionDto>>(powerConsumption);

            return Ok(powerConsumptionDto);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> SavePowerConsumprionData(string computerId, [FromBody] IEnumerable<PowerConsumptionCreationDto> input)
        {
            var powerConsumption = _mapper.Map<IEnumerable<PowerConsumption>>(input);
            powerConsumption.ToList().ForEach(p => p.ComputerId = computerId);

            await _repositoryContext.PowerConsumptions.AddRangeAsync(powerConsumption);
            await _repositoryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("collection/({powerConsumptionIds})")]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> DeletePowerConsumptionData(string computerId,
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> powerConsumptionIds)
        {
            var computer = HttpContext.Items["computer"] as Computer;

            var powerConsumptions = await _repositoryContext.PowerConsumptions
                .AsNoTracking()
                .Where(p => p.ComputerId == computerId && powerConsumptionIds.Contains(p.Id))
                .ToListAsync();

            if (!powerConsumptions.Any())
            {
                _logger.LogWarning($"There is no power consumption data with ids: {powerConsumptionIds}");
                return NotFound();
            }

            _repositoryContext.RemoveRange(powerConsumptions);
            await _repositoryContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
