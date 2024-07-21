using Microsoft.AspNetCore.Mvc;

#nullable disable
namespace MyApp.Namespace
{
    public class UserController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment;

        public UserController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment) {
            this.hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Index()
        {
            var userDao = new UserDao(hostingEnvironment.ContentRootPath);
            ViewBag.UserList = userDao.findAll();
            return View("Index");
        }

        public ActionResult Single(string id = "")
        {
            ActionResult result = null;
            var userDao = new UserDao(hostingEnvironment.ContentRootPath);
            var userData = userDao.find(id);
            if (userData == null) {
                result = RedirectToAction("Index", "User");
            } else {
                result = View(userData);
            }
            return result;
        }

        public ActionResult Edit(string id = "")
        {
            ActionResult result = null;
            var userDao = new UserDao(hostingEnvironment.ContentRootPath);
            var userData = userDao.find(id);
            if (userData == null) {
                result = RedirectToAction("Index", "User");
            } else {
                result = View(userData);
            }
            return result;
        }

        public ActionResult New()
        {
            var userData = new UserData();
            ViewData["message"] = "";
            return View(userData);
        }

        [HttpPost]
        public ActionResult Remove(string targetUserId)
        {
            var userDao = new UserDao(hostingEnvironment.ContentRootPath);
            var checkData = userDao.find(targetUserId);
            if (checkData == null) {
                ViewData["message"] = "指定したユーザIDはすでに削除済";
            } else {
                userDao.delete(checkData);
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult Register(UserData userData)
        {
            ActionResult result = null;
            if (userData.No == 0) {
                //新規モード
                var userDao = new UserDao(hostingEnvironment.ContentRootPath);
                var checkData = userDao.find(userData.UserId);
                if (checkData == null) {
                    userDao.insert(userData);
                    result = RedirectToAction("Index", "User");
                } else {
                    ViewData["message"] = "指定したユーザIDはすでに利用中";
                    result = View("New", userData);
                }
            } else {
                //編集モード
                var userDao = new UserDao(hostingEnvironment.ContentRootPath);
                var checkData = userDao.find(userData.UserId);
                if (checkData == null) {
                    ViewData["message"] = "指定したユーザIDはすでに削除済";
                    result = View("Edit", userData);
                } else {
                    userDao.update(userData);
                    result = RedirectToAction("Index", "User");
                }
            }
            return result;
        }

    }
}
