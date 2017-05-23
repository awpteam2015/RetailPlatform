using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Response
{
    public class UserInfoResponse
    {
        public virtual System.String UserCode { get; set; }

        /// <summary>
        /// 描述:用户名
        /// </summary>
        public virtual System.String UserName { get; set; }
        /// <summary>
        /// 描述:电子邮件
        /// </summary>
        public virtual System.String Email { get; set; }
        /// <summary>
        /// 描述:手机号
        /// </summary>
        public virtual System.String Mobile { get; set; }


        /// <summary>
        ///职务
        /// </summary>
        public virtual System.String Duty { get; set; }

        /// <summary>
        /// 等级 市级 镇级 区级
        /// </summary>
        public virtual System.String Lever { get; set; }


        /// <summary>
        /// 用户所在部门
        /// </summary>
        public virtual System.String Departments { get; set; }

        /// <summary>
        /// 角色 1代表管理员 2代表部门角色 3代表河长角色  4代表村管理员1007  5局办单位1008
        /// </summary>
        public virtual System.String Role { get; set; }
    }
}