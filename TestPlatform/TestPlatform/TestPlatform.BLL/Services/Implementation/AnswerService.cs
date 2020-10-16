using System.Collections.Generic;
using TestPlatform.BLL.Services.Interfaces;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.BLL.Services.Implementation
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository repository;

        public AnswerService(IAnswerRepository repo)
        {
            repository = repo;
        }

        public void UpdateAnswers(List<Answer> answers)
        {
            if(answers != null)
            {
                answers.ForEach(p => repository.UpdateAnswers(p));
            }
        }
    }
}
