using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Middleware
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var message = string.Join(" | ",context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                context.Result = new BadRequestObjectResult(new ApiResponse(400, message));
                return;
            }

            await next().ConfigureAwait(true);
        }
    }
}

