using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.ModelView
{
    public class PaginationBy : Pagination
    {
        public IEnumerable<SearchBy> searchBy { get; set; }
    }
}
