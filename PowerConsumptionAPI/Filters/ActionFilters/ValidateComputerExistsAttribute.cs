using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Filters.ActionFilters
{
    public class ValidateComputerExistsAttribute : IAsyncActionFilter
    {
        private readonly ILogger<ValidateComputerExistsAttribute> _logger;
        private readonly RepositoryContext _repositoryContext;

        public ValidateComputerExistsAttribute(ILogger<ValidateComputerExistsAttribute> logger, RepositoryContext repositoryContext)
        {
            _logger = logger;
            _repositoryContext = repositoryContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var id = (string)context.ActionArguments["computerId"]!;

            var computer = trackChanges ?
                await _repositoryContext.Computers.FindAsync(id) :
                await _repositoryContext.Computers.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);

            if (computer == null)
            {
                _logger.LogWarning($"Computer with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("computer", computer);
                await next();
            }
        }
    }
}
