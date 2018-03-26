

/***************************************************************************
*       功能：     CNMPageContentCategory实体类
*       作者：     李伟伟
*       日期：     2018/3/17
*       描述：     内容分类
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.WebMvc.Models.ExtendUi;

namespace Project.Model.ContentManager
{
    public class PageContentCategoryEntity : Entity, ITree
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String PageContentCategoryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 ParentId { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public virtual System.Int32? Rank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? Sort { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public virtual System.String Route { get; set; }


        public virtual System.String TitleOtherName1 { get; set; }
        public virtual System.String TitleOtherName2 { get; set; }
        public virtual System.String TitleOtherName3 { get; set; }

        public virtual System.String DescriptionOtherName1 { get; set; }
        public virtual System.String DescriptionOtherName2 { get; set; }
        public virtual System.String DescriptionOtherName3 { get; set; }


        public virtual System.String ImageUrlOtherName1 { get; set; }
        public virtual System.String ImageUrlOtherName2 { get; set; }
        public virtual System.String ImageUrlOtherName3 { get; set; }


        public virtual System.String IsShowTitle1 { get; set; }
        public virtual System.String IsShowTitle2 { get; set; }
        public virtual System.String IsShowTitle3 { get; set; }

        public virtual System.String IsShowDescription1 { get; set; }
        public virtual System.String IsShowDescription2 { get; set; }
        public virtual System.String IsShowDescription3 { get; set; }


        public virtual System.String IsShowImageUrl1 { get; set; }
        public virtual System.String IsShowImageUrl2 { get; set; }
        public virtual System.String IsShowImageUrl3 { get; set; }



        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual System.Int32 IsDeleted { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        public virtual System.String DeleterUserCode { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual System.DateTime? DeletionTime { get; set; }

public virtual System.String Tags { get; set; }
        #endregion


        #region 新增属性

        private string parentId;
        public virtual System.String _parentId
        {
            get { return (ParentId.ToString() == "0" || parentId == TreeInvalidCodeEnum.Invalid.ToString()) ? null : ParentId.ToString(); }
            set { this.parentId = value; }
        }

        public virtual ISet<PageContentCategoryEntity> children { get; set; }

        public virtual string id
        {
            get { return PkId.ToString(); }
        }

        public virtual string text
        {
            get { return PageContentCategoryName.ToString(); }
        }
        #endregion


    }
}




