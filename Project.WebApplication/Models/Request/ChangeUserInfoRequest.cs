using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class ChangeUserInfoRequest
    {
        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string Mobile { get; set; }
    }
}