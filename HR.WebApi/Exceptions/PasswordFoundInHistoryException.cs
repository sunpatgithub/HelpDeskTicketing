using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Exceptions
{
    public class PasswordFoundInHistoryException : Exception
    {
        public PasswordFoundInHistoryException()
            {
            }

    public PasswordFoundInHistoryException(string message)
        :base(message)
            {
            }

public PasswordFoundInHistoryException(string message, Exception inner)
        :base(message, inner)
            {
            }

    }
}
