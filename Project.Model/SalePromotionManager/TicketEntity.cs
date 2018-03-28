

 /***************************************************************************
 *       功能：     SPMTicket实体类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     券
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SalePromotionManager
{
    public class TicketEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String TicketCode{get; set;}
        /// <summary>
        /// 券类型编码
        /// </summary>
        public virtual System.String TickettypeId{get; set;}
        /// <summary>
        /// 激活 作废 已使用
        /// </summary>
        public virtual System.String Status{get; set;}
        /// <summary>
        /// 有效时间开始
        /// </summary>
        public virtual System.DateTime? AvaildateStart{get; set;}
        /// <summary>
        /// 有效时间结束
        /// </summary>
        public virtual System.DateTime? AvaildateEnd{get; set;}
        /// <summary>
        /// 使用订单号（2012.5.30新增）
        /// </summary>
        public virtual System.String OrderNo{get; set;}
        /// <summary>
        /// 使用日期（2012.5.30新增）
        /// </summary>
        public virtual System.DateTime? UseDate{get; set;}
        /// <summary>
        /// 归属会员
        /// </summary>
        public virtual System.Int32 CustomerId{get; set;}
        /// <summary>
        /// 活动规则Id
        /// </summary>
        public virtual System.Int32 RuleId{get; set;}
        /// <summary>
        /// 活动Id
        /// </summary>
        public virtual System.Int32 ActivityId{get; set;}

        /// <summary>
        /// 是否使用
        /// </summary>

        public virtual System.Int32 IsUse { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

