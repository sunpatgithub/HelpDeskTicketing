using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface ITicket<T>
    {
        Task<IEnumerable<T>> GetAll(int RecordLimit);

        Task<IEnumerable<T>> Get(int id);

        Task<IEnumerable<T>> FindPaginated(int pageIndex, int pageSize, string searchValue);

        int RecordCount(string searchValue);

        //Task<IList<T>> FindPaginatedList(int pageIndex, int pageSize, Expression<Func<T, bool>> expression);

        Task<T> Insert(T entity);

        Task<T> Update(T entity);

        Task<T> ToogleStatus(int id, string status);

        Task Delete(int id, string action, string comment);

        bool Exists(T entity);

        Task<T> ReassignTicketMethod(int ticketId, int deptId, string assignToId);
    }
}
