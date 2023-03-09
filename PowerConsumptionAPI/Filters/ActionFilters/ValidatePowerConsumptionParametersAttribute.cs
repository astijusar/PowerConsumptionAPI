using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.RequestFeatures;

namespace PowerConsumptionAPI.Filters.ActionFilters
{
    public class ValidatePowerConsumptionParametersAttribute : IActionFilter
    {
        private readonly ILogger<ValidatePowerConsumptionParametersAttribute> _logger;
        private readonly RepositoryContext _repositoryContext;

        public ValidatePowerConsumptionParametersAttribute(ILogger<ValidatePowerConsumptionParametersAttribute> logger, RepositoryContext repositoryContext)
        {
            _logger = logger;
            _repositoryContext = repositoryContext;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var parameters = (PowerConsumptionParameters)context.ActionArguments["parameters"];

            if (parameters != null)
            {
                if (!parameters.ValidInactivityRange)
                {
                    _logger.LogWarning("Max inactivity can't be less than min inactivity.");
                    context.Result = new BadRequestObjectResult("Max inactivity can't be less than min inactivity.");
                }
                else if (!parameters.ValidCpuRange)
                {
                    _logger.LogWarning("Max cpu power draw can't be less than min cpu power draw.");
                    context.Result = new BadRequestObjectResult("Max cpu power draw can't be less than min cpu power draw.");
                }
                else if (!parameters.ValidGpuRange)
                {
                    _logger.LogWarning("Max gpu power draw can't be less than min gpu power draw.");
                    context.Result = new BadRequestObjectResult("Max gpu power draw can't be less than min gpu power draw.");
                }
                else if (!parameters.ValidTotalRange)
                {
                    _logger.LogWarning("Max total power draw can't be less than min total power draw.");
                    context.Result = new BadRequestObjectResult("Max total power draw can't be less than min total power draw.");
                }
                else if (!parameters.ValidTimeRange)
                {
                    _logger.LogWarning("Max time can't be less than min time.");
                    context.Result = new BadRequestObjectResult("Max time can't be less than min time.");
                }
            }
        }
    }
}
