using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestPlatform.BLL.BusinessModels;
using TestPlatform.Common.ViewModels;
using TestPlatform.Common.Entities;
using TestPlatform.BLL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TestPlatform.WEB.Controllers
{
    public class CreatingController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ITestService testService;
        private readonly Handler handler;

        public CreatingController(ICategoryService categoryService, ITestService testService)
        {
            this.categoryService = categoryService;
            this.testService = testService;
            handler = new Handler();
        }

        public ViewResult GetListCategories()
        {
            return View(categoryService.Categories.Include(p => p.Test));
        }

        public IActionResult GetListTests(int id)
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

        public ViewResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.AddCategory(category);
                TempData["message"] = "Отлично!!! Категория успешно создана";
                return RedirectToAction(nameof(GetListTests), new { id = category.Id });
            }
            else
            {
                return View(category);
            }
        }

        public IActionResult CreateParamTest([FromForm] TestParamViewModel testModel)
        {
            if (testModel.Test != null)
            {
                return View(testModel);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult BuildQuestions([FromForm] TestParamViewModel testModel)
        {
            if (testModel.Test == null) return NotFound();

            if (testModel.Test.Question != null) return View(testModel);

            if (ModelState.IsValid)
            {
                return View(testModel);
            }
            else
            {
                return View(nameof(CreateParamTest), testModel);
            }
        }

        public IActionResult BuildAnswers([FromForm] TestParamViewModel testModel)
        {
            if (testModel.Test == null) return NotFound();

            if (ModelState.IsValid)
            {
                return View(testModel);
            }
            else
            {
                return View(nameof(BuildQuestions), testModel);
            }
        }

        [HttpPost]
        public IActionResult CreateTest(TestParamViewModel testModel)
        {
            List<Question> questions = testModel.Test.Question;

            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].Answer.Where(p => p.IsCorrect).Count() != 1)
                {
                    ModelState.AddModelError("", $"Правильным должен быть 1 вариант ответа (Вопрос №{i + 1})");
                }
            }

            if (ModelState.IsValid)
            {
                handler.CheckValue(testModel);
                testService.AddTest(testModel.Test);
                TempData["message"] = "Отлично!!! Тест успешно создан";
                return RedirectToAction(nameof(GetListTests), new { id = testModel.Test.CategoryId });
            }
            else
            {
                return View(nameof(BuildAnswers), testModel);
            }
        }
    }
}
