﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PowerConsumptionAPI.Filters.ActionFilters
{
    public class ValidationFilterAttribute : IAsyncActionFilter
    {
        private readonly ILogger<ValidationFilterAttribute> _logger;

        public ValidationFilterAttribute(ILogger<ValidationFilterAttribute> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if (param == null)
            {
                _logger.LogWarning($"Object sent from client is null. Controller: {controller}, action: {action}");
                context.Result = new BadRequestObjectResult("Object is null");

                return;
            }

            if (!context.ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid model state for the object. Controller: {controller}, action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
            else
            {
                await next();
            }
        }
    }
}
