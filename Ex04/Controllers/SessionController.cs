using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class SessionController : Controller
    {
        // GET: SessionController
        public ActionResult Index()
        {
            ViewData["Message"] = "SessionController Index!!";

            int? counter = HttpContext.Session.GetInt32("counter");
            HttpContext.Session.SetInt32("counter", ((counter.HasValue) ? (1 + counter.Value) : 0));

            return View();
        }

    }
}
