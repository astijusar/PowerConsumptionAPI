using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.DTOs.Computer;

namespace PowerConsumptionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly ILogger<ComputerController> _logger;
        private readonly IMapper _mapper;

        public ComputerController(RepositoryContext repositoryContext, ILogger<ComputerController> logger, IMapper mapper)
        {
            _repositoryContext = repositoryContext;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComputers()
        {
            var computers = await _repositoryContext.Computers
                .AsNoTracking()
                .ToListAsync();

            var computersDto = _mapper.Map<IEnumerable<ComputerDto>>(computers);

            return Ok(computersDto);
        }

        [HttpGet("{computerId}")]
        public async Task<IActionResult> GetComputer(string computerId)
        {
            var computer = await _repositoryContext.Computers
                .SingleOrDefaultAsync(c => c.Id == computerId);

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
            var computerCount = await _repositoryContext.Computers.CountAsync();
            computer.Name = $"PC{computerCount + 1}";

            _repositoryContext.Computers.Add(computer);
            await _repositoryContext.SaveChangesAsync();

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
            await _repositoryContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{computerId}")]
        [ServiceFilter(typeof(ValidateComputerExistsAttribute))]
        public async Task<IActionResult> DeleteComputer(string computerId)
        {
            var computer = HttpContext.Items["computer"] as Computer;

            _repositoryContext.Remove(computer);
            await _repositoryContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
