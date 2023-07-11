using Microsoft.AspNetCore.Mvc;

namespace BasicoPOS_MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
