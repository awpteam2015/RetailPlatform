﻿

 /***************************************************************************
 *       功能：     PRMSystemCategorySpec实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类--规格关联表
 * *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class SystemCategorySpecEntity: Entity
    {

        public  SystemCategorySpecEntity()
        {
           
        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SystemCategoryId{get; set;}

        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 Sort { get; set; }
        #endregion


        #region 新增属性

        public virtual SpecEntity SpecEntity { get; set; }

        #endregion
    }
}

    
 

