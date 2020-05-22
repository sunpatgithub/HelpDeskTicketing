using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Exceptions
{
    public class UserAlreadyExistException :Exception
    {
        public UserAlreadyExistException()
            {
            }

    public UserAlreadyExistException(string message)
        :base(message)
            {
            }

public UserAlreadyExistException(string message, Exception inner)
        :base(message, inner)
            {
            }
    }
}
