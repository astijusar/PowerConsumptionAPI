using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerConsumptionAPI.Models.DTOs.Limit;
using PowerConsumptionAPI.Models.DTOs.ElectricityCost;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Repository;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.ModelBinders;

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

        [HttpGet]
        public IActionResult GetElectricityCosts()
        {
            var electricityCosts = _repository.ElectricityCost.GetAllElectricityCosts(false);

            var electricityCostsDto = _mapper.Map<IEnumerable<ElectricityCostDto>>(electricityCosts);

            return Ok(electricityCostsDto);
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
            var electricityCosts = _repository.ElectricityCost.GetElectricityCostsById(electricityCostsIds, false);

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
