using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class LoginRequest
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户密码 md5
        /// </summary>
        public string UserPassword { get; set; }

    }
}