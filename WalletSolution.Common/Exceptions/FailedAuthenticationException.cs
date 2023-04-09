namespace WalletSolution.Common.Exceptions;
public class FailedAuthenticationException : Exception
{
    public FailedAuthenticationException() : base()
    {        
    }
    public FailedAuthenticationException(string message)
        : base(message)
    {        
    }
    public FailedAuthenticationException(string message, Exception innerException)
        : base(message, innerException)
    {        
    }
}
