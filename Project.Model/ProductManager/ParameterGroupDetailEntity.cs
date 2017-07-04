

 /***************************************************************************
 *       功能：     PRMParameterGroupDetail实体类
 *       作者：     李伟伟
 *       日期：     2017/7/4
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class ParameterGroupDetailEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 参数明细
        /// </summary>
        public virtual System.String ParameterName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? ParameterGroupId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SystemCategoryId{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

