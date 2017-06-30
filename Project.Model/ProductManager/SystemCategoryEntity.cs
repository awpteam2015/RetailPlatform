

 /***************************************************************************
 *       功能：     PRMSystemCategory实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class SystemCategoryEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 分类名称
        /// </summary>
        public virtual System.String SystemCategoryName{get; set;}
        /// <summary>
        /// 排序
        /// </summary>
        public virtual System.Int32? Sort{get; set;}
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime{get; set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode{get; set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual System.Int32? IsDeleted{get; set;}
        /// <summary>
        /// 删除人
        /// </summary>
        public virtual System.String DeleterUserCode{get; set;}
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual System.DateTime? DeletionTime{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

