using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Project.Application.Service.AccountManager;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
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

        public ActionResult ForgetPassword1()
        {
            return View();
        }

        /// <summary>
        /// 忘记密码第二页
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult ForgetPassword2(string key)
        {
            var result = new AccountServiceImpl().ForgetPassword2Validate(key);

            if (result.Item1)
            {
                ViewBag.MobilePhone = result.Item2;
                ViewBag.Key = key;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult ForgetPassword3(string key)
        {
            var result = new AccountServiceImpl().ForgetPassword3Validate(key);

            if (result.Item1)
            {
                ViewBag.MobilePhone = result.Item2;
                ViewBag.Key = key;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public ActionResult ForgetPassword4(string key)
        {

            return View();
        }
        #endregion


        #region 登录注册 
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string accountName, string password)
        {
            var userInfo = new AccountServiceImpl().Login(accountName, password);
            if (!userInfo.Item1)
            {
                return new AbpJsonResult
                {
                    Data = new AjaxResponse<object>() { success = false, error = new ErrorInfo("用户名或密码错误") }
                };
            }

            var ticket = new FormsAuthenticationTicket(
            1 /*version*/,
            Guid.NewGuid().ToString(),
            DateTime.Now,
            DateTime.Now.AddMinutes(30000),
            true,//持久性
            JsonConvert.SerializeObject(userInfo.Item2),
            FormsAuthentication.FormsCookiePath
            );
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.Expires = DateTime.Now.AddMinutes(30000);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);

            LoggerHelper.Info("用户登录：" + accountName + " 时间：" + DateTime.Now);

            return new AbpJsonResult
            {
                Data = new AjaxResponse<object>() { success = true }
            };
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="password"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(string accountName, string password, string authCode)
        {
            password = Encrypt.MD5Encrypt(password);
            var registResult = new AccountServiceImpl().Regist(accountName, password, authCode);

            var result = new AjaxResponse<object>()
            {
                success = registResult.Item1,
                error = new ErrorInfo(registResult.Item2)
            };

            LoggerHelper.Info("用户注册：" + accountName + " 时间：" + DateTime.Now);
            return new AbpJsonResult(result);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginOut(string accountName)
        {
            FormsAuthentication.SignOut();

            LoggerHelper.Info("用户登录：" + accountName + " 时间：" + DateTime.Now);

            return new AbpJsonResult
            {
                Data = new AjaxResponse<object>() { success = true }
            };
        }
        #endregion


        #region 忘记密码步骤
        /// <summary>
        /// 忘记密码1
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgetPasswordStep1(string accountName, string authCode)
        {
            var stepResult = new AccountServiceImpl().ForgetPasswordStep1(accountName, authCode, CookieHelper.GetValue("SSSSverifycode"));

            var result = new AjaxResponse<object>()
            {
                success = stepResult.Item1,
                result = stepResult.Item1 ? Server.UrlEncode(Encrypt.AESEncrypt(stepResult.Item2, Encrypt.GetKeyAES16())) : "",
                error = new ErrorInfo(stepResult.Item2)
            };

            return new AbpJsonResult(result);

        }



        /// <summary>
        /// 忘记密码2
        /// </summary>
        /// <param name="key"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgetPasswordStep2(string key, string authCode)
        {
            var stepResult = new AccountServiceImpl().ForgetPasswordStep2(key, authCode);
            var result = new AjaxResponse<object>()
            {
                success = stepResult.Item1,
                result = stepResult.Item1 ? Server.UrlEncode(Encrypt.AESEncrypt(stepResult.Item2, Encrypt.GetKeyAES16())) : "",
                error = new ErrorInfo(stepResult.Item2)
            };
            return new AbpJsonResult(result);

        }

        /// <summary>
        /// 忘记密码3
        /// </summary>
        /// <param name="key"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgetPasswordStep3(string key, string newPassword)
        {
            newPassword = Encrypt.MD5Encrypt(newPassword);
            var stepResult = new AccountServiceImpl().ForgetPasswordStep3(key, newPassword);
            var result = new AjaxResponse<object>()
            {
                success = stepResult.Item1,
                result = stepResult.Item1 ? Server.UrlEncode(Encrypt.AESEncrypt(stepResult.Item2, Encrypt.GetKeyAES16())) : "",
                error = new ErrorInfo(stepResult.Item2)
            };
            return new AbpJsonResult(result);

        }

        #endregion






    }
}