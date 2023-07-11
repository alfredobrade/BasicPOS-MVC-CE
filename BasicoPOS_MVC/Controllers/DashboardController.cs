using Microsoft.AspNetCore.Mvc;

namespace BasicoPOS_MVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
