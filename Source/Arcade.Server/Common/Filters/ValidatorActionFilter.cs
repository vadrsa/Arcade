using Common.Faults;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedEntities;

namespace Common.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
                throw new FaultException(FaultType.BadRequest, "test");
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}
