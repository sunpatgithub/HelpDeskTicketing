using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Exceptions
{
    public class PasswordExpiredException : Exception
    {
        public PasswordExpiredException()
            {
            }

    public PasswordExpiredException(string message)
        :base(message)
            {
            }

public PasswordExpiredException(string message, Exception inner)
        :base(message, inner)
            {
            }
    }
}
