using System.Linq;
using TestPlatform.BLL.Services.Interfaces;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.BLL.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository repository;

        public QuestionService(IQuestionRepository repo)
        {
            repository = repo;
        }

        public IQueryable<Question> Questions => repository.Questions;

        public void UpdateQuestion(Question question)
        {
            if(question != null)
            {
                repository.UpdateQuestion(question);
            }
        }

        public Question DeleteQuestion(int id)
        {
            Question question = Questions.FirstOrDefault(p => p.Id == id);

            if(question != null)
            {
                repository.DeleteQuestion(question);
            }

            return question;
        }

        public void AddQuestion(Question question)
        {
            if(question != null)
            {
                repository.AddQuestion(question);
            }
        }
    }
}
