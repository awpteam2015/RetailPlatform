﻿

 /***************************************************************************
 *       功能：     PRMProductSpec实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class ProductSpecEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? ProductId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecType{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

