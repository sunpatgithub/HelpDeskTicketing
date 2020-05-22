using System.Collections.Generic;

namespace HR.WebApi.ModelView
{
    public class ReturnBy<T> : Pagination
    {
       public IEnumerable<T> list { get; set; }
    }
}
