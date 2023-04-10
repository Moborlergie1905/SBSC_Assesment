using System.Net;

namespace WalletSolution.Common;
public class ApplicationException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public ApiResultStatusCode ApiStatusCode { get; set; }
    public object AdditionalData { get; set; }
    public ApplicationException() : this(ApiResultStatusCode.ServerError)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode)
            : this(statusCode, null)
    {        
    }
    public ApplicationException(string message)
            : this(ApiResultStatusCode.ServerError, message)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, string message)
            : this(statusCode, message, HttpStatusCode.InternalServerError)
    {        
    }
    public ApplicationException(string message, object additionalData)
            : this(ApiResultStatusCode.ServerError, message, additionalData)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, object additionalData)
            : this(statusCode, null, additionalData)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, string message, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
            : this(statusCode, message, httpStatusCode, null)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
            : this(statusCode, message, httpStatusCode, null, additionalData)
    {        
    }
    public ApplicationException(string message, Exception exception)
            : this(ApiResultStatusCode.ServerError, message, exception)
    {        
    }
    public ApplicationException(string message, Exception exception, object additionalData)
            : this(ApiResultStatusCode.ServerError, message, exception, additionalData)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, string message, Exception exception)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, string message, Exception exception, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
            : this(statusCode, message, httpStatusCode, exception, null)
    {        
    }
    public ApplicationException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
            : base(message, exception)
    {
        ApiStatusCode = statusCode;
        HttpStatusCode = httpStatusCode;
        AdditionalData = additionalData;
    }
}
