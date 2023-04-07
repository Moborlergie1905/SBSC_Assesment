namespace WalletSolution.Common.Exceptions;
public class InsertUpdateException : Exception
{
    public InsertUpdateException()
        : base()
    {
    }
    public InsertUpdateException(string message)
        : base(message)
    {
    }
    public InsertUpdateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
    public InsertUpdateException(string name, object key) //object key for option Insert/Update
        : base($"Entity \"{name}\" unable to {key}.")
    {
    }
}
