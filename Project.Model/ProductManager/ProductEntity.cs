

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
        /// 产品名称
        /// </summary>
        public virtual System.String ProductName{get; set;}
        /// <summary>
        /// 产品编码
        /// </summary>
        public virtual System.String ProductCode{get; set;}
        /// <summary>
        /// 单价
        /// </summary>
        public virtual System.Decimal? Price{get; set;}
        /// <summary>
        /// 商品分类
        /// </summary>
        public virtual System.Int32? ProductCategoryId{get; set;}
        /// <summary>
        /// 功率
        /// </summary>
        public virtual System.Int32? IsHasSpec1{get; set;}
        /// <summary>
        /// 颜色
        /// </summary>
        public virtual System.Int32? IsHasSpec2{get; set;}
        /// <summary>
        /// 其他
        /// </summary>
        public virtual System.Int32? IsHasSpec3{get; set;}
        /// <summary>
        /// 属性1
        /// </summary>
        public virtual System.String Attribute1{get; set;}
        /// <summary>
        /// 属性2
        /// </summary>
        public virtual System.String Attribute2{get; set;}
        /// <summary>
        /// 属性3
        /// </summary>
        public virtual System.String Attribute3{get; set;}
        /// <summary>
        /// 图片地址1
        /// </summary>
        public virtual System.String PicUrl1{get; set;}
        /// <summary>
        /// 图片地址2
        /// </summary>
        public virtual System.String PicUrl2{get; set;}
        /// <summary>
        /// 图片地址3
        /// </summary>
        public virtual System.String PicUrl3{get; set;}
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
        /// 备注
        /// </summary>
        public virtual System.String Remark{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

