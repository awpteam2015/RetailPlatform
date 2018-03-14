﻿

 /***************************************************************************
 *       功能：     PRMSpecValue实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     规格值
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class SpecValueEntity: Entity
    {
        #region 属性

        public virtual System.Int32? SpecValueId {
            get { return PkId; }
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecId{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecValueName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? Sort{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ImageUrl{get; set;}
        #endregion

    }
}

    
 

