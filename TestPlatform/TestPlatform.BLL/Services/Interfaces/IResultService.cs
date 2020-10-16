using System.Linq;
using TestPlatform.Common.Entities;

namespace TestPlatform.BLL.Services.Interfaces
{
    public interface IResultService
    {
        IQueryable<Result> Results { get; }
        void AddResult(Result result);
    }
}
