using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TheLair.ASP_Net.OneOfLogic.StatusCodes;

namespace TheLair.ASP_Net.OneOfLogic;

public class OneOfFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is not ObjectResult { Value: OneOf } obj) 
            return;

        OneOf data = (obj.Value as OneOf)!;

        context.Result = data.Value switch
        {
            Forbidden => new ForbidResult(),
            NotFound => new NotFoundResult(),
            _ => context.Result
        };
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}