using Microsoft.AspNetCore.Mvc;

#nullable disable
namespace MyApp.Namespace
{
    public class HomeController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment;

        public HomeController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment) {
            this.hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Index() {
            ActionResult result = View();
            if (HttpContext.Session.GetInt32("Login").HasValue) {
                result = RedirectToAction("Index", "User");
            } else {
                ViewData["message"] = "";
            }
            return result;
        }

        [HttpGet]
        public ActionResult Login() {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(string inputLoginId, string inputPassword) {
            ActionResult result = View("Index");
            // System.Console.WriteLine(inputLoginId);
            // System.Console.WriteLine(inputPassword);
            var loginDao = new LoginDao(hostingEnvironment.ContentRootPath);
            var loginData = loginDao.find(inputLoginId);
            if (loginData != null && loginData.Password == inputPassword) {
                HttpContext.Session.SetInt32("Login", 1);
                result = RedirectToAction("Index", "User");
            } else {
                ViewData["message"] = "ログイン失敗（ログイン情報が間違っている）";
            }
            return result;
        }

        public ActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
