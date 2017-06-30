

 /***************************************************************************
 *       功能：     PRMProductAttributeValue实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     产品--属性值关联表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class ProductAttributeValueEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? AttributeValueId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String AttributeValueName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? AttributeId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? ProductId{get; set;}
        /// <summary>
        /// 产品值内容
        /// </summary>
        public virtual System.String ValueContent{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

