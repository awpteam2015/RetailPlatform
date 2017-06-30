

 /***************************************************************************
 *       功能：     PRMBrand实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     品牌表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class BrandEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String BrandName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? Sort{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UrlLink{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Logo{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Remark{get; set;}
        /// <summary>
        /// 产品数量
        /// </summary>
        public virtual System.Int32? GoodsNum{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

