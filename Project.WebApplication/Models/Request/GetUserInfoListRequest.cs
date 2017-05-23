using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class GetUserInfoListRequest
    {

        /// <summary>
        /// 河道所属部门
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 1 转发角色
        /// 2 督办角色
        /// 3 转发+督办
        /// </summary>
        public int UserType { get; set; }
    }
}