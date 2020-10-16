using System.Collections.Generic;
using TestPlatform.Common.Entities;

namespace TestPlatform.BLL.Services.Interfaces
{
    public interface IAnswerService
    {
        void UpdateAnswers(List<Answer> answers);
    }
}
