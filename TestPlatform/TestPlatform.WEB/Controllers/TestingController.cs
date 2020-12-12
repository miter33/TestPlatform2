using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestPlatform.BLL.BusinessModels;
using TestPlatform.Common.ViewModels;
using TestPlatform.Common.Entities;
using TestPlatform.BLL.Services.Interfaces;
using System.Collections.Generic;

namespace TestPlatform.WEB.Controllers
{
    public class TestingController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ITestService testService;
        private readonly IQuestionService questionService;
        private readonly IResultService resultService;
        private readonly Handler handler;

        public TestingController(ICategoryService categoryService, ITestService testService, IQuestionService questionService, IResultService resultService)
        {
            this.categoryService = categoryService;
            this.testService = testService;
            this.questionService = questionService;
            this.resultService = resultService;
            handler = new Handler();
        }

        public ViewResult ChooseCategory()
        {
            return View(categoryService.Categories.Include(p => p.Test));
        }

        public IActionResult ChooseTest(int id)
        {
            Category category = categoryService.Categories.Include(p => p.Test).ThenInclude(p => p.Question).FirstOrDefault(p => p.Id == id);
            
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
                
                Test tests = testService.Tests.Include(p => p.Question).ThenInclude(p => p.Answer).FirstOrDefault(p => p.Id == result.TestId);
                result.Test = tests;
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
            List<Question> questions = questionService.Questions.Where(p => p.TestId == resultModel.Result.TestId).Include(p => p.Answer).ToList();

            if (questions.Any())
            {
                handler.CreatePoint(questions, resultModel);
                resultService.AddResult(resultModel.Result);
                resultModel.Test.Question = questions;
                return View("ShowResult", resultModel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
