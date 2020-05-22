using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IActivity<T>
    {
        Task Execute(T entity, string Status);
    }
}
