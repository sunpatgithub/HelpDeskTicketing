using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Exceptions
{
    public class TokenInvalidException :Exception
    {
        public TokenInvalidException()
            {
            }

    public TokenInvalidException(string message)
        :base(message)
            {
            }

public TokenInvalidException(string message, Exception inner)
        :base(message, inner)
            {
            }
    }
}
