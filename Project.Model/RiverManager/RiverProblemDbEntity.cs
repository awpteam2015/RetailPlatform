

 /***************************************************************************
 *       功能：     RMRiverProblemDb实体类
 *       作者：     李伟伟
 *       日期：     2016/8/13
 *       描述：     督办
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class RiverProblemDbEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 督办意见信息
        /// </summary>
        public virtual System.String DbRemark{get; set;}
        /// <summary>
        /// 督办人
        /// </summary>
        public virtual System.String UserCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CreateTime{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? RiverProblemApplyId{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

