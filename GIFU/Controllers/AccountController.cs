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
        /// 註冊頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View(new Models.Account());
        }

        /// <summary>
        /// 註冊頁面處理
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(Models.Account account)
        {
            if (ModelState.IsValid)
            {
                int result = accountServices.AddAccount(account);
                if (result > 0)
                {
                    RedirectToAction("Login");
                    TempData["result"] = 1;
                    TempData["message"] = "註冊成功!!";
                    return View(account);
                }
            }
            TempData["result"] = -1;
            TempData["message"] = "輸入錯誤，請重新輸入一次。";
            return View(account);
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Store");
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
            Session["username"] = account.Name;
            Session["userId"] = account.UserId;

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
            if (User.Identity.IsAuthenticated)
            {
                //清除Session中的資料
                Session.Abandon();
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);
                FormsAuthentication.SignOut();
            }
            //FormsAuthentication.RedirectToLoginPage();
            Response.Redirect(FormsAuthentication.LoginUrl);
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