using TestPlatform.Common.Entities;
using TestPlatform.DAL.Context;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.DAL.Repositories.Implementation
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly TestDbContext context;

        public AnswerRepository(TestDbContext ctx)
        {
            context = ctx;
        }

        public void UpdateAnswers(Answer answer)
        {
            context.Answers.Update(answer);
            context.SaveChanges();
        }
    }
}
