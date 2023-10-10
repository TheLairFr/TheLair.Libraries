using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TheLair.ASP_Net.OneOfLogic;

public class OneOfFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is not ObjectResult { Value: OneOf } obj) 
            return;

        OneOf data = (obj.Value as OneOf)!;

        if (data.Value is StatusCodeResult value)
            context.Result = value;
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}