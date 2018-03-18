

 /***************************************************************************
 *       功能：     SMCity实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SystemSetManager
{
    public class CityEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CityId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String City{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ProvinceId{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

