using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface ITicketLog<T>
    {
        Task<IEnumerable<T>> GetAll(int RecordLimit);

        Task<IEnumerable<T>> Get(int id);

        Task Insert(T entity);
        
    }
}
