using HR.WebApi.ModelView;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IReportBuilderRun<T>
    {
        string Get(int id);

        Task<ReturnBy<T>> Execute(string strQuery);
    }
}
