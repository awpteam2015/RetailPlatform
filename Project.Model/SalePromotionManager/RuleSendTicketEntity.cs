﻿

 /***************************************************************************
 *       功能：     SPMRuleSendTicket实体类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     满足金额发券
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SalePromotionManager
{
    public class RuleSendTicketEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 活动规则Id
        /// </summary>
        public virtual System.Int32 RuleId{get; set;}
        /// <summary>
        /// 活动Id
        /// </summary>
        public virtual System.Int32 ActivityId{get; set;}
        /// <summary>
        /// 订单金额范围
        /// </summary>
        public virtual System.Int32 StartMoney{get; set;}
        /// <summary>
        /// 订单金额范围
        /// </summary>
        public virtual System.Int32 EndMoney{get; set;}
        /// <summary>
        /// 券张数
        /// </summary>
        public virtual System.Int32 TicketNum{get; set;}
        /// <summary>
        /// 会员可类型范围
        /// </summary>
        public virtual System.String CardTypeIds{get; set;}
        /// <summary>
        /// 有效时间结束
        /// </summary>
        public virtual System.DateTime? TicketAvaildateEnd{get; set;}
        /// <summary>
        /// 有效时间开始
        /// </summary>
        public virtual System.DateTime? TicketAvaildateStart{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

