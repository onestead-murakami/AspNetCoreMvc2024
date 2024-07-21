using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index() {
            ViewData["Message"] = "HomeController Index!!";
            return View();
        }
        public ActionResult List() {
            ViewData["Message"] = "HomeController List!!";
            return View();
        }

    }
}
