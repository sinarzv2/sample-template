using System.Linq;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SinaRazavi_Test.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is BadRequestObjectResult { Value: ValidationProblemDetails value } badRequestObjectResult)
            {
                var apiResult = new ApiResult<object>();
                var errorMessages = value.Errors.SelectMany(p => p.Value).Distinct();
                apiResult.AddErrors(errorMessages);
                context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
            }

            base.OnResultExecuting(context);
        }
    }
}
