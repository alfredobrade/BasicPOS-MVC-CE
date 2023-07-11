using Microsoft.AspNetCore.Mvc;

namespace BasicoPOS_MVC.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
