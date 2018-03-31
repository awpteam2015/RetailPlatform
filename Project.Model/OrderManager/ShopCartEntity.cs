

 /***************************************************************************
 *       功能：     OMShopCart实体类
 *       作者：     李伟伟
 *       日期：     2018/3/31
 *       描述：     购物车
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.OrderManager
{
    public class ShopCartEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 订单号
        /// </summary>
        public virtual System.String OrderNo{get; set;}
        /// <summary>
        /// 商品分类
        /// </summary>
        public virtual System.Int32 ProductCategoryId{get; set;}
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual System.String ProductName{get; set;}
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
        /// 图片地址
        /// </summary>
        public virtual System.String ImageUrl{get; set;}
        /// <summary>
        /// 商品原价
        /// </summary>
        public virtual System.Decimal Price{get; set;}
        /// <summary>
        /// 会员折扣
        /// </summary>
        public virtual System.Decimal DiscountMember{get; set;}
        /// <summary>
        /// 促销折扣
        /// </summary>
        public virtual System.Decimal DiscountPromotion{get; set;}
        /// <summary>
        /// 积分折扣
        /// </summary>
        public virtual System.Decimal DiscountPoint{get; set;}
        /// <summary>
        /// 总折扣
        /// </summary>
        public virtual System.Decimal DiscountAll{get; set;}
        /// <summary>
        /// 促销价
        /// </summary>
        public virtual System.Decimal PromotionPrice{get; set;}
        /// <summary>
        /// 最后成交单价
        /// </summary>
        public virtual System.Decimal LastPrice{get; set;}
        /// <summary>
        /// 单项小计
        /// </summary>
        public virtual System.Decimal TotalAmount{get; set;}
        /// <summary>
        /// 商品重量
        /// </summary>
        public virtual System.Decimal ProductWeight{get; set;}
        /// <summary>
        /// 规格汇总
        /// </summary>
        public virtual System.String SpecDetail { get; set;}
        /// <summary>
        /// 商品数量
        /// </summary>
        public virtual System.Int32 Num{get; set;}
        /// <summary>
        /// 是否选中
        /// </summary>
        public virtual System.Int32 IsCheck{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 CustomerId{get; set;}
        /// <summary>
        /// 促销规则Id
        /// </summary>
        public virtual System.Int32 RuleId{get; set;}

        /// <summary>
        /// 是否过期
        /// </summary>
        public virtual System.Int32 IsExpire { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

