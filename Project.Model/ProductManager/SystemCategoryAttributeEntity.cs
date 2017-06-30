

 /***************************************************************************
 *       功能：     PRMSystemCategoryAttribute实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类对应属性
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class SystemCategoryAttributeEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? AttributeId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SystemCategoryId{get; set;}
        /// <summary>
        /// 0不是必须的1是必须的
        /// </summary>
        public virtual System.Int32? IsMust{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

