

 /***************************************************************************
 *       功能：     CNMOfflineActivity实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ContentManager
{
    public class OfflineActivityEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 活动标题
        /// </summary>
        public virtual System.String Tttle{get; set;}
        /// <summary>
        /// 活动地址
        /// </summary>
        public virtual System.String OfflineActivityAddress{get; set;}
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual System.DateTime? StartDate{get; set;}
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual System.DateTime? EndDate{get; set;}
        /// <summary>
        /// 图片
        /// </summary>
        public virtual System.String ImageUrl{get; set;}
        /// <summary>
        /// 简介
        /// </summary>
        public virtual System.String BriefDescription{get; set;}

        public virtual System.String Description { get; set;}
        /// <summary>
        /// 活动状态
        /// </summary>
        public virtual System.Int32? State{get; set;}
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
        public virtual System.String BindGoodsCode { get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

