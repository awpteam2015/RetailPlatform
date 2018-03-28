

 /***************************************************************************
 *       功能：     SPMRulePromotionGoods实体类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     促销商品
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SalePromotionManager
{
    public class RulePromotionGoodsEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 活动Id
        /// </summary>
        public virtual System.Int32 ActivityId{get; set;}
        /// <summary>
        /// 活动规则Id
        /// </summary>
        public virtual System.Int32 RuleId{get; set;}
        /// <summary>
        /// 产品主键
        /// </summary>
        public virtual System.Int32 ProductId{get; set;}
        /// <summary>
        /// 产品编码
        /// </summary>
        public virtual System.String ProductCode{get; set;}
        /// <summary>
        /// 商品代码
        /// </summary>
        public virtual System.String GoodsCode{get; set;}
        /// <summary>
        /// 商品主键
        /// </summary>
        public virtual System.Int32 GoodsId{get; set;}
        /// <summary>
        /// 单价
        /// </summary>
        public virtual System.Decimal Price{get; set;}
        /// <summary>
        /// 促销价
        /// </summary>
        public virtual System.Decimal PromotionPrice{get; set;}


        public virtual System.String ProductName { get; set; }
        public virtual System.String SpecDetail { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

