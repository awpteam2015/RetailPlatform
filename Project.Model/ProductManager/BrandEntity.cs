

 /***************************************************************************
 *       功能：     PRMBrand实体类
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     品牌
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
        public virtual System.String BrandDes{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

