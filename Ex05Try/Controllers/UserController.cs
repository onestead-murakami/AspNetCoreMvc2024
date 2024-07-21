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

        /// <summary>ユーザー情報一覧</summary>
        public ActionResult Index()
        {
            var userDao = new UserDao(hostingEnvironment.ContentRootPath);
            ViewBag.UserList = userDao.findAll();
            return View("Index");
        }

        /// <summary>ユーザー情報詳細</summary>
        /// <param name="id">ユーザーID</param>
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

        /// <summary>ユーザー情報編集</summary>
        /// <param name="id">ユーザーID</param>
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

        /// <summary>ユーザー情報追加</summary>
        public ActionResult New()
        {
            var userData = new UserData();
            ViewData["message"] = "";
            return View(userData);
        }

        /// <summary>ユーザー情報削除</summary>
        /// <param name="targetUserId">ユーザーID</param>
        [HttpPost]
        public ActionResult Remove(string targetUserId)
        {
            var userDao = new UserDao(hostingEnvironment.ContentRootPath);
            var checkData = userDao.find(targetUserId);
            if (checkData == null) {
                Console.WriteLine("指定したユーザIDはすでに削除済");
                ViewData["message"] = "指定したユーザIDはすでに削除済";
            } else {
                // データ削除機能を追加
                // データ削除機能を追加
                // データ削除機能を追加
            }
            return RedirectToAction("Index", "User");
        }

        /// <summary>ユーザー情報登録</summary>
        /// <param name="userData">ユーザーデータ</param>
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
                    // データ編集機能を追加
                    // データ編集機能を追加
                    // データ編集機能を追加
                }
            }
            return result;
        }

    }
}
