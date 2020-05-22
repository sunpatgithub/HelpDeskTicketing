using HR.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class ReturnView<T> : Pagination
    {
        public IEnumerable<T> ListData { get; set; }
    }
}
