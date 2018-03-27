

 /***************************************************************************
 *       功能：     SPMRule实体类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     促销规则
 * *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SalePromotionManager
{
    public class RuleEntity: Entity
    {

        public RuleEntity()
        {
            RuleDiscountMoneyEntity=new RuleDiscountMoneyEntity();
            RuleSendTicketEntity=new RuleSendTicketEntity();
            RulePromotionGoodsEntityList=new HashSet<RulePromotionGoodsEntity>();
        }


        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 ActivityId{get; set;}
        /// <summary>
        /// 规则类型A B C
        /// </summary>
        public virtual System.String RuleType{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Title{get; set;}
        #endregion


        #region 新增属性
        public virtual string Kind
        {
            get { return "Rule"; }
        }

        /// <summary>
        /// 折扣条件
        /// </summary>
        public virtual RuleDiscountMoneyEntity RuleDiscountMoneyEntity { get; set; }

        /// <summary>
        /// 发送券条件
        /// </summary>
        public virtual RuleSendTicketEntity RuleSendTicketEntity { get; set; }

        /// <summary>
        ///促销商品
        /// </summary>
        public virtual ISet<RulePromotionGoodsEntity> RulePromotionGoodsEntityList { get; set; }
        #endregion
    }
}

    
 

