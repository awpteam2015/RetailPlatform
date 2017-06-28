﻿

 /***************************************************************************
 *       功能：     PRMGoods实体类
 *       作者：     李伟伟
 *       日期：     2017/5/27
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
        public virtual System.Int32? ProductId{get; set;}
        /// <summary>
        /// 商品编码
        /// </summary>
        public virtual System.String GoodsCode{get; set;}
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual System.String GoodsName{get; set;}
        /// <summary>
        /// 产品编码
        /// </summary>
        public virtual System.String ProductCode {get; set;}
        /// <summary>
        /// 功率
        /// </summary>
        public virtual System.String SpecValue1{get; set;}
        /// <summary>
        /// 颜色
        /// </summary>
        public virtual System.String SpecValue2{get; set;}
        /// <summary>
        /// 其他
        /// </summary>
        public virtual System.String SpecValue3{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

