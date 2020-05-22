using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Exceptions
{
    public class RecordAlreadyExistException : Exception
    {
        public RecordAlreadyExistException()
            {
            }

    public RecordAlreadyExistException(string message)
        :base(message)
            {
            }

public RecordAlreadyExistException(string message, Exception inner)
        :base(message, inner)
            {
            }
    }
}
