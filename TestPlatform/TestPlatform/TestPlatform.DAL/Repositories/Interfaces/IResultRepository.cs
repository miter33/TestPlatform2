using System.Linq;
using TestPlatform.Common.Entities;

namespace TestPlatform.DAL.Repositories.Interfaces
{
    public interface IResultRepository
    {
        IQueryable<Result> Results { get; }
        void AddResult(Result result);
    }
}
