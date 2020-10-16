using System.Linq;
using TestPlatform.Common.Entities;

namespace TestPlatform.DAL.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        IQueryable<Question> Questions { get; }
        void UpdateQuestion(Question question);
        void DeleteQuestion(Question question);
        void AddQuestion(Question question);
    }
}
