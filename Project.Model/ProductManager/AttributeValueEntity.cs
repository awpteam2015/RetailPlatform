

 /***************************************************************************
 *       功能：     PRMAttributeValue实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     扩展属性值
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class AttributeValueEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String AttributeValueName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? AttributeId{get; set;}

        /// <summary>
        /// 排序
        /// </summary>
        public virtual System.Int32? Sort { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

