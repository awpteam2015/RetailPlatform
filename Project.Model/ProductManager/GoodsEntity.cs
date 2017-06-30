

 /***************************************************************************
 *       功能：     PRMGoods实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     商品表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class GoodsEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? ProductId{get; set;}
        /// <summary>
        /// 货号
        /// </summary>
        public virtual System.String GoodsCode{get; set;}
        /// <summary>
        /// 库存
        /// </summary>
        public virtual System.Int32? GoodsStock{get; set;}
        /// <summary>
        /// 销售价
        /// </summary>
        public virtual System.Decimal? GoodsPrice{get; set;}
        /// <summary>
        /// 成本价
        /// </summary>
        public virtual System.Decimal? GoodsCost{get; set;}
        /// <summary>
        /// 重量
        /// </summary>
        public virtual System.Int32? GoodsWeight{get; set;}
        /// <summary>
        /// 重量单位
        /// </summary>
        public virtual System.String GoodsWeightUnit{get; set;}
        /// <summary>
        /// 单位
        /// </summary>
        public virtual System.String Unit{get; set;}
        /// <summary>
        /// 商品描述
        /// </summary>
        public virtual System.String Title{get; set;}
        /// <summary>
        /// 是否是默认商品
        /// </summary>
        public virtual System.Int32? IsDefault{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

