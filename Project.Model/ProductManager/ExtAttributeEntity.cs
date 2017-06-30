

 /***************************************************************************
 *       功能：     PRMExtAttribute实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     扩展属性表
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class ExtAttributeEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String AttributeName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String OtherName{get; set;}
        /// <summary>
        /// 表现方式 1 select 2input 3
        /// </summary>
        public virtual System.Byte? ShowType{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String AttributeValues{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

