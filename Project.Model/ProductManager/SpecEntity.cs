

 /***************************************************************************
 *       功能：     PRMSpec实体类
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     规格
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class SpecEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecValues{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

