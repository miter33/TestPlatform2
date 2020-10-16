using System.Linq;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Context;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.DAL.Repositories.Implementation
{
    public class ResultRepository : IResultRepository
    {
        private readonly TestDbContext context;

        public ResultRepository(TestDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Result> Results => context.Results;
        
        public void AddResult(Result result)
        {
            context.Results.Add(result);
            context.SaveChanges();
        }
    }
}
