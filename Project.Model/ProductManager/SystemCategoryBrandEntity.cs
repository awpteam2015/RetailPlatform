

 /***************************************************************************
 *       功能：     PRMSystemCategoryBrand实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类对应品牌
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class SystemCategoryBrandEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? BrandId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SystemCategoryId{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

