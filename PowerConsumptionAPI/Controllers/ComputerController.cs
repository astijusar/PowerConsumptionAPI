using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.DTOs.Computer;
using PowerConsumptionAPI.Repository;

namespace PowerConsumptionAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<ComputerController> _logger;
        private readonly IMapper _mapper;

        public ComputerController(IRepositoryManager repository, ILogger<ComputerController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComputers()
        {
            var computers = await _repository.Computer.GetAllComputersAsync(false);

            var computersDto = _mapper.Map<IEnumerable<ComputerDto>>(computers);

            return Ok(computersDto);
        }

        [HttpGet("{computerId}")]
        public async Task<IActionResult> GetComputer(string computerId)
        {
            var computer = await _repository.Computer.GetComputerAsync(computerId, false);

            if (computer == null)
            {
                _logger.LogWarning($"Computer with id: {computerId} does not exist in the database.");
                return NotFound();
            }

            var computerDto = _mapper.Map<ComputerDto>(computer);

            return Ok(computerDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateComputer([FromBody] ComputerCreationDto input)
        {
            var computer = _mapper.Map<Computer>(input);
            computer.Name = computer.Id;

            _repository.Computer.CreateComputer(computer);
            await _repository.SaveAsync();

            var computerDto = _mapper.Map<ComputerDto>(computer);

            return CreatedAtAction("GetComputer", new { computerId = computerDto.Id }, computerDto);
        }

        [HttpPut("{computerId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> UpdateComputer(string computerId, [FromBody] ComputerUpdateDto input)
        {
            var computer = HttpContext.Items["computer"] as Computer;

            _mapper.Map(input, computer);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{computerId}")]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> DeleteComputer(string computerId)
        {
            var computer = HttpContext.Items["computer"] as Computer;

            _repository.Computer.DeleteComputer(computer);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
