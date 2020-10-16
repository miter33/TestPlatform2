using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Context;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.DAL.Repositories.Implementation
{
    public class TestRepository : ITestRepository
    {
        private readonly TestDbContext context;

        public TestRepository(TestDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Test> Tests => context.Tests.Include(p => p.Category);

        public void AddTest(Test test)
        {
            context.Tests.Add(test);
            context.SaveChanges();
        }

        public void UpdateTest(Test test)
        {
            context.Tests.Update(test);
            context.SaveChanges();
        }

        public void DeleteTest(Test test)
        {
            context.Tests.Remove(test);
            context.SaveChanges();
        }
    }
}
