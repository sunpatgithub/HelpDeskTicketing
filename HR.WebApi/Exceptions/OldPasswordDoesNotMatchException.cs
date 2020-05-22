using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Exceptions
{
    public class OldPasswordDoesNotMatchException : Exception
    {
        public OldPasswordDoesNotMatchException()
        {
        }

        public OldPasswordDoesNotMatchException(string message)
            : base(message)
        {
        }

        public OldPasswordDoesNotMatchException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
