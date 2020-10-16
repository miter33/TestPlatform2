using System.Linq;
using TestPlatform.BLL.Services.Interfaces;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.BLL.Services.Implementation
{
    public class TestService : ITestService
    {
        private readonly ITestRepository repository;

        public TestService(ITestRepository repo)
        {
            repository = repo;
        }

        public IQueryable<Test> Tests => repository.Tests;

        public void AddTest(Test test)
        {
            if(test != null)
            {
                repository.AddTest(test);
            }
        }

        public void UpdateTest(Test test)
        {
            if(test != null)
            {
                repository.UpdateTest(test);
            }
        }

        public Test DeleteTest(int id)
        {
            var test = Tests.FirstOrDefault(p => p.Id == id);

            if(test != null)
            {
                repository.DeleteTest(test);
            }

            return test;
        }
    }
}
