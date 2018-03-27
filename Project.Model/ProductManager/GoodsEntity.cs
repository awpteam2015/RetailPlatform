

 /***************************************************************************
 *       功能：     PRMGoods实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     商品表
 * *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class GoodsEntity: Entity
    {


        public GoodsEntity()
        {
            GoodsSpecValueList=new HashSet<GoodsSpecValueEntity>();
        }

        #region 属性
        /// <summary>
        /// 组合规格的sku编码
        /// </summary>
        public virtual string CombineId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 ProductId{get; set;}
        /// <summary>
        /// 货号
        /// </summary>
        public virtual System.String GoodsCode{get; set;}

        /// <summary>
        /// Sku编码
        /// </summary>
        public virtual System.String SkuCode { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public virtual System.Int32 GoodsStock{get; set;}
        /// <summary>
        /// 销售价
        /// </summary>
        public virtual System.Decimal GoodsPrice{get; set;}
 
        /// <summary>
        /// 是否是默认商品
        /// </summary>
        public virtual System.Int32 IsDefault{get; set;}

        /// <summary>
        /// 规格值明细
        /// </summary>
        public virtual System.String SpecDetail { get; set; }
        #endregion


        #region 新增属性

        public virtual ISet<GoodsSpecValueEntity> GoodsSpecValueList{ get; set;}


        public virtual System.Int32 IsUse { get; set; }
        #endregion
    }
}

    
 

