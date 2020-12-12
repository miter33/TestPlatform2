using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestPlatform.BLL.Services.Interfaces;
using TestPlatform.Common.Entities;

namespace TestPlatform.WEB.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IResultService resultService;
        private readonly ICategoryService categoryService;

        public StatisticsController(IResultService resultService, ICategoryService categoryService)
        {
            this.resultService = resultService;
            this.categoryService = categoryService;
        }

        public ViewResult ShowStatsByCategories()
        {
            return View(resultService.Results.Include(p => p.Test).ThenInclude(p => p.Category));
        }

        public IActionResult ShowStatsByTests(int id)
        {
            Category category = categoryService.Categories.Include(p => p.Test).ThenInclude(p => p.Result).FirstOrDefault(p => p.Id == id);
            
            if (category != null)
            {
                return View(category);
            }
            else
            {
                return NotFound();
            }
        }

        public ViewResult ShowGeneralUserStats()
        {
            return View(resultService.Results);
        }

        public IActionResult ShowUserStatsByCategories(string userName)
        {
            IQueryable<Result> results = resultService.Results.Include(p => p.Test).ThenInclude(p => p.Category).Where(p => p.UserName == userName);
            
            if(results.Any())
            {
                return View(results);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult ShowUserStatsByTests(string userName, int id)
        {
            IQueryable<Result> results = resultService.Results.Include(p => p.Test).ThenInclude(p => p.Category).Where(p => p.UserName == userName && p.Test.CategoryId == id);
            
            if (results.Any())
            {
                return View(results);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
