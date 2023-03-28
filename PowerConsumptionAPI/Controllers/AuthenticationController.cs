using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PowerConsumptionAPI.Filters.ActionFilters;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.DTOs.User;
using PowerConsumptionAPI.Repository;
using PowerConsumptionAPI.Services;
using System.Xml.Linq;

namespace PowerConsumptionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IRepositoryManager _repository;

        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<User> userManager,
            IAuthenticationManager authManager, IRepositoryManager repository)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _repository = repository;
        }

        [HttpGet("computer/{computerId}/test")]
        public async Task<IActionResult> TestComputer(string computerId)
        {
            var computer = await _repository.Computer.GetComputerAsync(computerId, false);

            if (computer == null)
            {
                _logger.LogWarning($"Computer with id: {computerId} does not exist in the database.");
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken() });
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return StatusCode(201);
        }
    }
}
