using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletSolution.Common.Exceptions;
public class InsufficentFundException : Exception
{
    public InsufficentFundException() 
        : base()
    {        
    }
    public InsufficentFundException(string message) 
        : base(message)
    {        
    }
    public InsufficentFundException(string message, Exception innerException) 
        : base(message, innerException)
    {        
    }
}
