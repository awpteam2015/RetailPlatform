

 /***************************************************************************
 *       功能：     PRMSpec实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     规格表
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
        public virtual System.String Memo{get; set;}
        /// <summary>
        /// 0text 1image
        /// </summary>
        public virtual System.Byte? SpecType{get; set;}
        /// <summary>
        /// 0平铺 1下拉框
        /// </summary>
        public virtual System.Byte? ShowType{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

