

 /***************************************************************************
 *       功能：     CMCustomerCollection实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.CustomerManager
{
    public class CustomerCollectionEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? CustomerId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? ProductId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ProductName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? GoodsId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String GoodsCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ProductCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ImageUrl{get; set;}
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

    
 

