using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Exceptions
{
    public class RecoredNotFoundException : Exception
    {

        public RecoredNotFoundException()
            {
            }

    public RecoredNotFoundException(string message)
        :base(message)
            {
            }

public RecoredNotFoundException(string message, Exception inner)
        :base(message, inner)
            {
            }
    }
}
