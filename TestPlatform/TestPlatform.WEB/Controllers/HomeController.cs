using Microsoft.AspNetCore.Mvc;

namespace TestPlatform.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
