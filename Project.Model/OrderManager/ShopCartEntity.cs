

 /***************************************************************************
 *       功能：     OMShopCart实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
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
        public virtual System.Int32? ProductCategoryId{get; set;}
        /// <summary>
        /// 产品主键
        /// </summary>
        public virtual System.Int32? ProductId{get; set;}
        /// <summary>
        /// 商品代码
        /// </summary>
        public virtual System.String GoodsCode{get; set;}
        /// <summary>
        /// 商品主键
        /// </summary>
        public virtual System.String GoodsId{get; set;}
        /// <summary>
        /// 商品原价_decimal_
        /// </summary>
        public virtual System.Decimal? Price{get; set;}
        /// <summary>
        /// 购买价_decimal_
        /// </summary>
        public virtual System.Decimal? PriceSubDiscount{get; set;}
        /// <summary>
        /// 单项小计_decimal_
        /// </summary>
        public virtual System.Decimal? TotalAmount{get; set;}
        /// <summary>
        /// 商品重量
        /// </summary>
        public virtual System.Decimal? ProductWeight{get; set;}
        /// <summary>
        /// 规格汇总
        /// </summary>
        public virtual System.String SpecName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 CustomerId{get; set;}


        public virtual System.Int32 IsCheck { get; set; }

        public virtual System.Decimal PromotionPrice { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

