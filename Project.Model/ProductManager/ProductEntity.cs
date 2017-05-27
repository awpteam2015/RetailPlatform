

 /***************************************************************************
 *       功能：     PRMProduct实体类
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     产品表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class ProductEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ProductName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ProductCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Decimal? Price{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? ProductCategoryId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecId1{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecId2{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecId3{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecName1{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecName2{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecName3{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Attribute1{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Attribute2{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Attribute3{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String PicUrl1{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String PicUrl2{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String PicUrl3{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

