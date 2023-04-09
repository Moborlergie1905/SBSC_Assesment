using Microsoft.AspNetCore.Mvc.Filters;
using WalletSolution.APIFramework.Tools;
using WalletSolution.Common.Exceptions;

namespace WalletSolution.API.Filters;
public class ApiExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilter()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ExistingRecordException), HandleExistingRecordException },
                { typeof(InsertUpdateException), HandleInsertUpdateException },
                { typeof(InsufficentFundException), HandleInsufficientFundException },
            };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        context.Result = new ApiResult<int>(-1, StatusCodes.Status500InternalServerError, new string[] {
            "an error occurred", context.Exception.Message });

        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidationException;
        List<string> errorList = new List<string>();
        foreach (var error in exception.Errors.Values)
        {
            errorList.AddRange(error);
        }

        context.Result = new ApiResult<int>(-1, StatusCodes.Status400BadRequest, errorList.ToArray());

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;

        context.Result = new ApiResult<int>(-1, StatusCodes.Status404NotFound, new string[] { "your resource not found ", exception.Message });

        context.ExceptionHandled = true;
    }
    private void HandleExistingRecordException(ExceptionContext context)
    {
        var exception = context.Exception as ExistingRecordException;

        context.Result = new ApiResult<int>(-1, StatusCodes.Status400BadRequest, new string[] { exception.Message });

        context.ExceptionHandled = true;
    }
    private void HandleInsertUpdateException(ExceptionContext context)
    {
        var exception = context.Exception as InsertUpdateException;

        context.Result = new ApiResult<int>(-1, StatusCodes.Status500InternalServerError, new string[] { exception.Message });

        context.ExceptionHandled = true;
    }
    private void HandleInsufficientFundException(ExceptionContext context)
    {
        var exception = context.Exception as InsufficentFundException;

        context.Result = new ApiResult<int>(-1, StatusCodes.Status400BadRequest, new string[] { exception.Message });

        context.ExceptionHandled = true;
    }
}
