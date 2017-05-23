

/***************************************************************************
*       功能：     RMRiverCheck实体类
*       作者：     李伟伟
*       日期：     2016/7/23
*       描述：     巡查信息
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class RiverCheckEntity : Entity, IAudited
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? RiverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String RiverName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserCode { get; set; }
        /// <summary>
        /// 控制点
        /// </summary>
        public virtual System.String Coords { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual System.Int32? IsActive { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }


        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }


        public virtual System.String PicUrl1 { get; set; }

        public virtual System.String PicUrl2 { get; set; }

        public virtual System.String PicUrl3 { get; set; }
        #endregion


        #region 新增属性
        public virtual string Att_CreationTime
        {
            get
            {
                if (CreationTime != null)
                {
                    return CreationTime.GetValueOrDefault().ToString("MM-dd");
                }
                return "";
            }
        }

        public virtual IList<string> Attr_DepartmentCodes { get; set; }


        public virtual DateTime Attr_BeginDate { get; set; }

        public virtual DateTime Attr_EndDate { get; set; }


        public virtual string Attr_RiverDepartmentName { get; set; }

        public virtual string Attr_Lever { get; set; }


        #endregion
    }
}




