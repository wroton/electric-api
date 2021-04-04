using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Service.Server.Infrastructure
{
    /// <summary>
    /// Validates the model for a controller.
    /// </summary>
    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called when the action with the attribute is executing.
        /// </summary>
        /// <param name="context">Context of the action.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
