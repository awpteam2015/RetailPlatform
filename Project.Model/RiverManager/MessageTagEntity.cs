

 /***************************************************************************
 *       功能：     RMMessageTag实体类
 *       作者：     李伟伟
 *       日期：     2016/9/11
 *       描述：     消息是否已读标记
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class MessageTagEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? Kind{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserCode{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserName{get; set;}
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? MessageId{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

