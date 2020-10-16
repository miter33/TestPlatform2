using System.Linq;
using TestPlatform.Common.Entities;

namespace TestPlatform.BLL.Services.Interfaces
{
    public interface ITestService
    {
        IQueryable<Test> Tests { get; }
        void AddTest(Test test);
        void UpdateTest(Test test);
        Test DeleteTest(int id);
    }
}
