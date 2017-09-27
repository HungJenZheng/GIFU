using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GIFU.Controllers
{
    public class AccountController : Controller
    {
        private Models.AccountServices accountServices = new Models.AccountServices();

        // GET: Account
        [Authorize]
        public ActionResult Index(string ReturnUrl)
        {
            return View();
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                RedirectToAction("Index", "Store");
            return View(new Models.LoginVM());
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Models.LoginVM loginVM)
        {
            //檢驗帳號、密碼為必輸
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "請輸入帳號密碼!";
                return View(loginVM);
            }

            loginVM.ShaPasswd = FormsAuthentication.HashPasswordForStoringInConfigFile(loginVM.Passwd, "SHA1");
            //檢查帳號是否存在
            Models.Account account = accountServices.Authentication(loginVM);
            if (account.Email == null)
            {
                TempData["Error"] = "您輸入的帳號不存在或者密碼錯誤!";
                return View(loginVM);
            }

            Session.RemoveAll();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                account.Email,  //User.Identity.Name
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                account.UserId.ToString(),
                FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);

            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            //以下只會執行一個
            FormsAuthentication.RedirectFromLoginPage(loginVM.Email, false);
            //string returnRul = FormsAuthentication.GetRedirectUrl(loginVM.Email, false).TrimEnd('/');
            return Redirect(FormsAuthentication.GetRedirectUrl(loginVM.Email, false));
        }

        /// <summary>
        /// 登出
        /// </summary>
        [Authorize]
        public void SignOut()
        {
            //清除Session中的資料
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        /// <summary>
        /// 取得帳戶詳細資料
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAccountDetailById(string accountId)
        {
            int id = Convert.ToInt32(accountId);
            var account = accountServices.GetAccountDetailById(id);
            return this.Json(account);
        }

        /// <summary>
        /// 新增帳戶
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddAccount(Models.Account account)
        {
            if (ModelState.IsValid)
            {
                int result = accountServices.AddAccount(account);
                return this.Json(result);
            }
            return this.Json(-1);
        }

        /// <summary>
        /// 更新帳戶
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateAccount(Models.Account account)
        {
            if (ModelState.IsValid && account.UserId != null)
            {
                int result = accountServices.UpdateAccount(account);
                return this.Json(result);
            }
            return this.Json(-1);
        }
    }
}