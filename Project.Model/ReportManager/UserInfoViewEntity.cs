using System;

namespace Project.Model.ReportManager
{
    [Serializable]
    public class UserInfoViewEntity
    {
        #region 属性


        /// <summary>
        /// 描述:员工号
        /// </summary>
        public virtual System.String UserCode { get; set; }

        /// <summary>
        /// 描述:用户名
        /// </summary>
        public virtual System.String UserName { get; set; }


        /// <summary>
        /// 描述:手机号
        /// </summary>
        public virtual System.String Mobile { get; set; }

        /// <summary>
        ///职务
        /// </summary>
        public virtual System.String Duty { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public virtual System.String Lever { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// 河道名称
        /// </summary>
        public virtual System.String RiverName { get; set; }
        #endregion


    }
}
