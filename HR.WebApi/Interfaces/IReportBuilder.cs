using HR.WebApi.ModelView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IReportBuilder<T>
    {
        Task<IEnumerable<T>> GetAll(int RecordLimit);

        Task<IEnumerable<T>> Get(int id);

        Task<ReturnBy<T>> GetPaginated(PaginationBy paginationBy);

        Task Insert(T entity);

        Task Update(T entity);

        Task Delete(int id);

        bool Exists(T entity);
    }
}
