﻿

/***************************************************************************
*       功能：     PMDepartment实体类
*       作者：     李伟伟
*       日期：     2015/12/22
*       描述：     部门基础信息表
*--------------------------------------修改记录-----------------------------
*1. Roy 加入类型机构或是科室
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.WebMvc.Models.ExtendUi;
using Project.Model.Enum;

namespace Project.Model.PermissionManager
{
    public class DepartmentEntity : Entity, ITree
    {
        #region 属性
        /// <summary>
        /// 部门编码
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public virtual System.String ParentDepartmentCode { get; set; }


        public virtual System.Int32 DepartmentType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }

        #endregion


        #region 新增属性
        public virtual IList<DepartmentEntity> children { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public virtual string id
        {
            get { return DepartmentCode; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String text
        {
            get { return DepartmentName; }
        }



        private string parentId;
        public virtual System.String _parentId
        {
            get { return (ParentDepartmentCode == "0" || parentId == TreeInvalidCodeEnum.Invalid.ToString()) ? null : ParentDepartmentCode; }
            set { this.parentId = value; }
        }

        public virtual bool Attr_IsCheck { get; set; }
        public virtual int Attr_UserDepartmentPkId { get; set; }

        public virtual string ParentDepartmentName { get; set; }
        #endregion
        public virtual string Mobile { get; set; }
        // public virtual string _parentId { get; set; }
    }
}




