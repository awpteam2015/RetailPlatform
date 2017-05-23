

/***************************************************************************
*       功能：     RMMsgNotice实体类
*       作者：     李伟伟
*       日期：     2016/7/30
*       描述：     消息通知
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class MsgNoticeEntity : Entity, IAudited
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Des { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? IsDelete { get; set; }
        /// <summary>
        /// 是否已发送 0代表未发送
        /// </summary>
        public virtual System.Int32? IsSend { get; set; }
        /// <summary>
        /// 发送日期
        /// </summary>
        public virtual System.DateTime? SendTime { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public virtual bool IsRead { get; set; }


        public virtual System.String BelongCompanys { get; set; }
        #endregion


        #region 新增属性

        public virtual string Attr_IsSend
        {
            get
            {
                switch (IsSend)
                {
                    case 1:
                        return "是";
                    default:
                        return "否";
                }
            }
        }

        #endregion
    }
}




