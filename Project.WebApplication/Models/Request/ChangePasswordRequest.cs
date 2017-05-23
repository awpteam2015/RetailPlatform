using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class ChangePasswordRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserCode { get; set; }

        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
    }
}