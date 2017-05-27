

 /***************************************************************************
 *       功能：     PRMProductCategory实体类
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     商品分类
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class ProductCategoryEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 分类名称
        /// </summary>
        public virtual System.String ProductCategoryName{get; set;}
        /// <summary>
        /// 父级分类
        /// </summary>
        public virtual System.Int32? ParentProductCategoryId{get; set;}
        /// <summary>
        /// 分类路径
        /// </summary>
        public virtual System.String CategoryRoute{get; set;}
        /// <summary>
        /// 分类层级
        /// </summary>
        public virtual System.Int32? Rank{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

