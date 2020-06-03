using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IEmployee_BasicInfo<T>
    {
        Task<IEnumerable<T>> GetAll(int RecordLimit);
    }
}
