

 /***************************************************************************
 *       功能：     CNMPageContent实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ContentManager
{
    public class PageContentEntity: Entity
    {
        #region 属性
        public virtual System.Int32 PageContentCategoryId { get; set; }

        public virtual System.String PageContentCategoryName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Title1{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Title2{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Title3{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Description1{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Description2{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Description3{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ImageUrl1{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ImageUrl2{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ImageUrl3{get; set;}
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual System.DateTime? DeletionTime{get; set;}
        /// <summary>
        /// 删除人
        /// </summary>
        public virtual System.String DeleterUserCode{get; set;}
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual System.Int32? IsDeleted{get; set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime{get; set;}
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}

        /// <summary>
        /// 浏览量
        /// </summary>
        public virtual System.String BrowseCount { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

