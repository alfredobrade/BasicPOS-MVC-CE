using Microsoft.AspNetCore.Mvc;

namespace BasicoPOS_MVC.Controllers
{
    public class BusinessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
