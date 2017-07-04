﻿

 /***************************************************************************
 *       功能：     PRMSystemCategory实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类
 * *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public sealed class SystemCategoryEntity: Entity
    {

        public SystemCategoryEntity()
        {
            SystemCategoryBrandList=new List<SystemCategoryBrandEntity>();
            SystemCategorySpecList=new List<SystemCategorySpecEntity>();
            SystemCategoryAttributeList=new List<SystemCategoryAttributeEntity>();
            ParameterGroupList=new List<ParameterGroupEntity>();
            ParameterGroupDetailList=new List<ParameterGroupDetailEntity>();
        }

        #region 属性
        /// <summary>
        /// 分类名称
        /// </summary>
        public System.String SystemCategoryName{get; set;}
        /// <summary>
        /// 排序
        /// </summary>
        public System.Int32? Sort{get; set;}
        /// <summary>
        /// 创建人
        /// </summary>
        public System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreationTime{get; set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public System.String LastModifierUserCode{get; set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? LastModificationTime{get; set;}
        /// <summary>
        /// 是否删除
        /// </summary>
        public System.Int32? IsDeleted{get; set;}
        /// <summary>
        /// 删除人
        /// </summary>
        public System.String DeleterUserCode{get; set;}
        /// <summary>
        /// 删除时间
        /// </summary>
        public System.DateTime? DeletionTime{get; set;}
        #endregion


        #region 新增属性
        /// <summary>
        /// 系统类型对应品牌
        /// </summary>
        public IList<SystemCategoryBrandEntity> SystemCategoryBrandList { get; set; }

        /// <summary>
        /// 系统类型对应规格
        /// </summary>
        public IList<SystemCategorySpecEntity> SystemCategorySpecList { get; set; }


        /// <summary>
        /// 系统类型对应属性
        /// </summary>
        public IList<SystemCategoryAttributeEntity> SystemCategoryAttributeList { get; set; }

        /// <summary>
        /// 系统类型对应参数组
        /// </summary>
        public IList<ParameterGroupEntity> ParameterGroupList { get; set; }


        /// <summary>
        /// 系统类型对应参数明细
        /// </summary>
        public IList<ParameterGroupDetailEntity> ParameterGroupDetailList { get; set; }

        #endregion
    }
}

    
 

