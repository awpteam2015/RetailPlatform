

 /***************************************************************************
 *       功能：     PRMGoodsSpecValue实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     商品-规格值关联表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class GoodsSpecValueEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? GoodsId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? ProductId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecValueId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecValueName{get; set;}

        public virtual System.String SpecValueOtherName { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

