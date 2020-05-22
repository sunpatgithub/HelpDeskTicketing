using HR.WebApi.ModelView;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IPaginated<T>
    {
        Task<ReturnBy<T>> GetPaginated(PaginationBy paginationBy);
    }
}
