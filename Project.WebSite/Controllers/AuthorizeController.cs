﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.PermissionManager;
using Project.Service.CustomerManager.Dto;
using Project.Service.PermissionManager;
using Project.Service.PermissionManager.DTO;

namespace Project.WebSite.Controllers
{
    /// <summary>
    /// 已登录页
    /// </summary>
    public class AuthorizeController : BaseController
    {
        public AuthorizeController()
        {
            CustomerDto=new CustomerDto()
            {
                CustomerId = 1,
                CustomerName = "Test",
                Mobilephone = "电话"
            };

        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public CustomerDto CustomerDto { get; set; }



        ///// <summary>
        ///// 在调用操作方法前调用。
        ///// </summary>
        ///// <param name="filterContext">有关当前请求和操作的信息。</param>
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (filterContext == null)
        //    {
        //        throw new ArgumentNullException("filterContext");
        //    }

        //    var userData = ((FormsIdentity)User.Identity).Ticket.UserData;
        //    if (userData != "")
        //    {
        //        CustomerDto = JsonConvert.DeserializeObject<CustomerDto>(userData);
        //    }

        //    base.OnActionExecuting(filterContext);
        //}


        ///// <summary>
        ///// 在进行授权时调用。
        ///// </summary>
        ///// <param name="filterContext">有关当前请求和操作的信息。</param>
        //protected override void OnAuthentication(AuthenticationContext filterContext)
        //{
        //    if (!HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = new ContentResult { Content = @"<script>window.top.location='/Login/Index3'</script>" };
        //        base.OnAuthentication(filterContext);
        //        return;
        //    }

        //    //身份验证
        //    ViewBag.ShowInfo += "OnAuthentication<br/>";
        //    base.OnAuthentication(filterContext);
        //}


    }
}