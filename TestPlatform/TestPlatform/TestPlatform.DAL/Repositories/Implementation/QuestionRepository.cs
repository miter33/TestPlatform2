using System.Linq;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Context;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.DAL.Repositories.Implementation
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly TestDbContext context;

        public QuestionRepository(TestDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Question> Questions => context.Questions;

        public void UpdateQuestion(Question question)
        {
            context.Questions.Update(question);
            context.SaveChanges();
        }

        public void DeleteQuestion(Question question)
        {
            context.Questions.Remove(question);
            context.SaveChanges();
        }

        public void AddQuestion(Question question)
        {
            context.Questions.Add(question);
            context.SaveChanges();
        }
    }
}
