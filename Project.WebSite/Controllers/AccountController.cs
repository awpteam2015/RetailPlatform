using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.CustomerManager;

namespace Project.WebSite.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string accountName, string password)
        {
            LoggerHelper.Info("登陆前：");
            var userInfo =CustomerService.GetInstance().Login(accountName, password);
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
            LoggerHelper.Info("登陆前：");
            var userInfo = CustomerService.GetInstance().Login(mobilephone, password);
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



    }
}