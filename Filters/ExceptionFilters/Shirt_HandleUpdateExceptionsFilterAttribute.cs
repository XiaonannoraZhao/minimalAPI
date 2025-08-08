using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using minimalAPI.Models.Repositories;


namespace minimalAPI.Filters.ExceptionFilters
{
    public class Shirt_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var strShirtId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strShirtId, out int shirtId))
            {
                if (!ShirtRepository.ShirtExists(shirtId))
                {
                    context.ModelState.AddModelError("ShirtId", $"Shirt with id {shirtId} does not exist.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
                else
                {
                    context.Result = new BadRequestObjectResult($"An error occurred while updating the shirt with id {shirtId}.");
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid Shirt Id.");
            }
            
            

        }

    }
}