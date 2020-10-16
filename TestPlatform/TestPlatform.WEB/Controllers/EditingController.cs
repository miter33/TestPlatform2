using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestPlatform.BLL.BusinessModels;
using TestPlatform.Common.ViewModels;
using TestPlatform.Common.Entities;
using TestPlatform.BLL.Services.Interfaces;

namespace TestPlatform.WEB.Controllers
{
    public class EditingController : Controller
    {
        private readonly ICategoryService catService;
        private readonly ITestService testService;
        private readonly IQuestionService questService;
        private readonly IAnswerService answerService;
        private readonly Handler handler;

        public EditingController(ITestService testService, IQuestionService questService, ICategoryService catService, IAnswerService answerService)
        {
            this.testService = testService;
            this.questService = questService;
            this.catService = catService;
            this.answerService = answerService;
            handler = new Handler();
        }

        public ViewResult ChooseActionCategories()
        {
            return View(catService.Categories.Include(p => p.Test));
        }

        public IActionResult EditCategory(int id)
        {
            var category = catService.Categories.FirstOrDefault(p => p.Id == id);

            if (category != null)
            {
                return View(category);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                catService.UpdateCategory(category);
                TempData["message"] = "Отлично!!! Название категории успешно сохранено";
                return RedirectToAction(nameof(ChooseActionCategories));
            }
            else
            {
                return View(nameof(EditCategory), category);
            }
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var deletedCategory = catService.DeleteCategory(categoryId);

            if (deletedCategory != null)
            {
                TempData["message"] = $"Отлично!!! Категория \"{deletedCategory.Name}\" успешно удалена";
            }

            return RedirectToAction(nameof(ChooseActionCategories));
        }

        public IActionResult ChooseActionTests(int id)
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

        [HttpPost]
        public IActionResult UpdateTest(Test test)
        {
            if (ModelState.IsValid)
            {
                test.Category = null;
                testService.UpdateTest(test);
                TempData["message"] = "Отлично!!! Параметры теста успешно сохранены";
                return RedirectToAction(nameof(ChooseActionTests), new { id = test.CategoryId });
            }
            else
            {
                return View(nameof(EditTest), test);
            }
        }

        [HttpPost]
        public IActionResult DeleteTest(int testId, int categoryId)
        {
            var deletedTest = testService.DeleteTest(testId);

            if (deletedTest != null)
            {
                TempData["message"] = $"Отлично!!! Тест \"{deletedTest.Name}\" успешно удалён";
            }

            return RedirectToAction(nameof(ChooseActionTests), new { id = categoryId });
        }

        public IActionResult EditTest(int id)
        {
            var test = testService.Tests.FirstOrDefault(p => p.Id == id);

            if (test != null)
            {
                return View(test);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult ChooseActionQuestions(int id)
        {
            var test = testService.Tests.Include(p => p.Question).ThenInclude(p => p.Answer).FirstOrDefault(p => p.Id == id);

            if (test != null)
            {
                return View(test);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult EditQuestion(int id)
        {
            var question = questService.Questions.Include(p => p.Test).ThenInclude(p => p.Category).FirstOrDefault(p => p.Id == id);

            if (question != null)
            {
                return View(question);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult UpdateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                question.Test = null;
                questService.UpdateQuestion(question);
                TempData["message"] = "Отлично!!! Название вопроса успешно сохранено";
                return RedirectToAction(nameof(ChooseActionQuestions), new { id = question.TestId });
            }
            else
            {
                return View(nameof(EditQuestion), question);
            }
        }

        [HttpPost]
        public IActionResult DeleteQuestion(int questionId, int testId)
        {
            var deletedQuestion = questService.DeleteQuestion(questionId);

            if(deletedQuestion != null)
            {
                TempData["message"] = $"Отлично!!! Вопрос \"{deletedQuestion.Name}\" успешно удалён";
            }

            return RedirectToAction(nameof(ChooseActionQuestions), new { id = testId });
        }

        public IActionResult BuildQuestion([FromForm] TestParamViewModel testModel)
        {
            if (testModel.Test == null) return NotFound();

            testModel.CountQuestions = 1;
            return View(testModel);
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
                return View(nameof(BuildQuestion), testModel);
            }
        }

        [HttpPost]
        public IActionResult CreateQuestion(TestParamViewModel testModel)
        {
            var questionList = testModel.Test.Question;

            if (questionList.FirstOrDefault().Answer.Where(p => p.IsCorrect).Count() != 1)
            {
                ModelState.AddModelError("", $"Правильным должен быть 1 вариант ответа");
            }

            if (ModelState.IsValid)
            {
                handler.CheckValue(testModel);
                questService.AddQuestion(testModel.Test.Question.FirstOrDefault());
                TempData["message"] = "Отлично!!! Вопрос успешно добавлен";
                return RedirectToAction(nameof(ChooseActionQuestions), new { id = testModel.Test.Id });
            }
            else
            {
                return View(nameof(BuildAnswers), testModel);
            }
        }

        public IActionResult ChooseActionAnswers(int id)
        {
            var question = questService.Questions.Include(p => p.Test).ThenInclude(p => p.Category).Include(p => p.Answer).FirstOrDefault(p => p.Id == id);

            if (question != null)
            {
                return View(question);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult UpdateAnswers(Question question)
        {
            if (question.Answer.Where(p => p.IsCorrect).Count() != 1 && !question.IsOpenType)
            {
                ModelState.AddModelError("", $"Правильным должен быть 1 вариант ответа");
            }

            if (ModelState.IsValid)
            {
                answerService.UpdateAnswers(question.Answer);
                TempData["message"] = "Отлично!!! Изменения успешно сохранены";
                return RedirectToAction(nameof(ChooseActionQuestions), new { id = question.TestId });
            }
            else
            {
                return View(nameof(ChooseActionAnswers), question);
            }
        }
    }
}
