using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerConsumptionAPI.Models.DTOs.Limit;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Repository;
using PowerConsumptionAPI.Filters.ActionFilters;

namespace PowerConsumptionAPI.Controllers
{
    [Route("api/electricity_cost")]
    [ApiController]
    public class ElectricityCostController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<ElectricityCostController> _logger;
        private readonly IMapper _mapper;

        public ElectricityCostController(IRepositoryManager repository, ILogger<ElectricityCostController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("limit")]
        public IActionResult GetLimits()
        {
            var limits = _repository.Limit.GetAllLimits(LimitType.Cost, false);

            var limitsDto = _mapper.Map<IEnumerable<LimitDto>>(limits);

            return Ok(limitsDto);
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

        [HttpDelete("limit")]
        public IActionResult DeleteLimit(Guid limitId)
        {
            var limit = _repository.Limit.GetLimitById(limitId, LimitType.Cost, false);

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
