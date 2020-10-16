using System.Linq;
using TestPlatform.Common.Entities;

namespace TestPlatform.DAL.Repositories.Interfaces
{
    public interface ITestRepository
    {
        IQueryable<Test> Tests { get; }
        void AddTest(Test test);
        void UpdateTest(Test test);
        void DeleteTest(Test test);
    }
}
