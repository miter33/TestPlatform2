using System.Linq;
using TestPlatform.BLL.Services.Interfaces;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.BLL.Services.Implementation
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository repository;

        public ResultService(IResultRepository repo)
        {
            repository = repo;
        }

        public IQueryable<Result> Results => repository.Results;

        public void AddResult(Result result)
        {
            if(result != null)
            {
                repository.AddResult(result);
            }
        }
    }
}
