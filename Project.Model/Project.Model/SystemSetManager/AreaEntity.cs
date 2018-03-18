

 /***************************************************************************
 *       功能：     SMArea实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SystemSetManager
{
    public class AreaEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String AreaId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Area{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CityId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? FirstWeightPrice{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SecondWeightPrice{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

