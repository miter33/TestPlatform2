using System;
using System.Collections.Generic;
using System.Linq;
using TestPlatform.Common.ViewModels;
using TestPlatform.Common.Entities;

namespace TestPlatform.BLL.BusinessModels
{
    public class Handler
    {
        public void CheckValue(TestParamViewModel testModel)
        {
            for (int i = 0; i < testModel.Test.Question.Count; i++)
            {
                if(testModel.CountAnswerViewModel[i].CountAnswers == 1)
                {
                    testModel.Test.Question[i].IsOpenType = true;
                }
            }

            testModel.Test.Category = null;
        }

        public void CreatePoint(List<Question> question, ResultParamViewModel resultModel)
        {
            double point = 0;
            var listRes = resultModel.UserAnswer;

            for (int i = 0; i < question.Count; i++)
            {
                if (listRes[i] == null) continue;

                if(listRes[i].ToLower() == question[i].Answer.FirstOrDefault(p => p.IsCorrect).Name.ToLower())
                {
                    point++;
                }
            }

            point = Math.Round((point * 100) / question.Count, 1);
            resultModel.Result.Point = point;

            if(resultModel.Result.Point >= resultModel.Test.Rate)
            {
                resultModel.Result.IsSuccess = true;
            }
        }
    }
}
