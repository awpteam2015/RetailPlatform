using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.ImageHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.CustomerManager;

namespace Project.WebSite.Controllers
{
    public class AccountController : Controller
    {
        #region
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        #endregion



        [HttpPost]
        public ActionResult Login(string accountName, string password)
        {
            LoggerHelper.Info("登陆前：");
            var userInfo = CustomerService.GetInstance().Login(accountName, password);
            if (!userInfo.Item1)
            {
                return new AbpJsonResult
                {
                    Data = new AjaxResponse<object>() { success = false, error = new ErrorInfo(userInfo.Item2) }
                };
            }

            var ticket = new FormsAuthenticationTicket(
            1 /*version*/,
            Guid.NewGuid().ToString(),
            DateTime.Now,
            DateTime.Now.AddMinutes(30000),
            true,//持久性
            FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.Expires = DateTime.Now.AddMinutes(30000);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);

            //  FormsAuthentication.SetAuthCookie(FormsAuthentication.FormsCookieName,false);

            LoggerHelper.Info("登陆结束：");
            return new AbpJsonResult
            {
                Data = new AjaxResponse<object>() { success = true }
            };
        }


        [HttpPost]
        public ActionResult Register(string mobilephone, string password)
        {

            return new AbpJsonResult();
        }


        public ActionResult VerifyCode(int height = 0)
        {
            var image = new VerificationImage();
            var authCode = StringHelper.GetRnd(4, true, true, true, false, "");
            image.Width = 70;
            if (height > 0 && height <= 30)
            {
                image.Height = height;
            }
            else
            {
                image.Height = 30;
            }
            image.BorderWidth = 1;
            image.BadPiont = 200;
            image.StrukLineCount = 8;
            image.PatternCount = 6;
            image.BorderInsetColor = System.Drawing.Color.LightGray;
            image.BorderOutsetColor = System.Drawing.Color.Empty;
            image.Text = authCode;
            image.CreateImage();

            CookieHelper.Set("xsjverifycode", 1, authCode);
            return File(image.Stream.ToArray(), @"image/jpeg");
        }



    }
}