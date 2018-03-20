

 /***************************************************************************
 *       功能：     PRMProductCategory实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     
 * *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.WebMvc.Models.ExtendUi;

namespace Project.Model.ProductManager
{
    public class ProductCategoryEntity : Entity, ITree
    {
        public ProductCategoryEntity()
        {
            children=new HashSet<ProductCategoryEntity>();
        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ProductCategoryName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? ParentId{get; set;}
        /// <summary>
        /// 层级
        /// </summary>
        public virtual System.Int32? Rank{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? Sort{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SystemCategoryId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SystemCategoryName{get; set;}
        /// <summary>
        /// 路径
        /// </summary>
        public virtual System.String Route{get; set;}
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
        private string parentId;
        public virtual System.String _parentId
        {
            get { return (ParentId.ToString() == "0" || parentId == TreeInvalidCodeEnum.Invalid.ToString()) ? null : ParentId.ToString(); }
            set { this.parentId = value; }
        }

        public virtual ISet<ProductCategoryEntity> children { get; set; }


        /// <summary>
        /// 部门编码
        /// </summary>
        public virtual string id
        {
            get { return PkId.ToString(); }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String text
        {
            get { return ProductCategoryName; }
        }
        #endregion
    }
}

    
 

