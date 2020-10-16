using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestPlatform.BLL.BusinessModels;
using TestPlatform.Common.ViewModels;
using TestPlatform.Common.Entities;
using TestPlatform.BLL.Services.Interfaces;

namespace TestPlatform.WEB.Controllers
{
    public class TestingController : Controller
    {
        private readonly ICategoryService catService;
        private readonly ITestService testService;
        private readonly IQuestionService questService;
        private readonly IResultService resService;
        private readonly Handler handler;

        public TestingController(ICategoryService catService, ITestService testService, IQuestionService questService, IResultService resService)
        {
            this.catService = catService;
            this.testService = testService;
            this.questService = questService;
            this.resService = resService;
            handler = new Handler();
        }

        public ViewResult ChooseCategory()
        {
            return View(catService.Categories.Include(p => p.Test));
        }

        public IActionResult ChooseTest(int id)
        {
            var category = catService.Categories.Include(p => p.Test).ThenInclude(p => p.Question).FirstOrDefault(p => p.Id == id);
            
            if (category != null)
            {
                return View(category);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult CreateUserName([FromForm] Result result)
        {
            if (result.TestId != 0)
            {
                return View(result);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Solve([FromForm] Result result)
        {
            if (result.TestId == 0) return NotFound();

            if (ModelState.IsValid)
            {
                var listTest = testService.Tests.Include(p => p.Question).ThenInclude(p => p.Answer).FirstOrDefault(p => p.Id == result.TestId);
                result.Test = listTest;
                return View(result);
            }
            else
            {
                return View(nameof(CreateUserName), result);
            }
        }

        [HttpPost]
        public IActionResult CreateResult(ResultParamViewModel resultModel)
        {
            var listQuestion = questService.Questions.Where(p => p.TestId == resultModel.Result.TestId).Include(p => p.Answer).ToList();

            if (listQuestion.Any())
            {
                handler.CreatePoint(listQuestion, resultModel);
                resService.AddResult(resultModel.Result);
                resultModel.Test.Question = listQuestion;
                return View("ShowResult", resultModel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
