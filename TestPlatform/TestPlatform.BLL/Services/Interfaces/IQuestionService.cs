using System.Linq;
using TestPlatform.Common.Entities;

namespace TestPlatform.BLL.Services.Interfaces
{
    public interface IQuestionService
    {
        IQueryable<Question> Questions { get; }
        void UpdateQuestion(Question question);
        Question DeleteQuestion(int id);
        void AddQuestion(Question question);
    }
}
