

 /***************************************************************************
 *       功能：     SMAuthCode实体类
 *       作者：     李伟伟
 *       日期：     2018/3/20
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SystemSetManager
{
    public class AuthCodeEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 验证码类别
        /// </summary>
        public virtual System.String AuthType{get; set;}
        /// <summary>
        /// 发送方式
        /// </summary>
        public virtual System.String SendType{get; set;}
        /// <summary>
        /// 接收人
        /// </summary>
        public virtual System.String ReciviceUser{get; set;}
        /// <summary>
        /// 验证码
        /// </summary>
        public virtual System.String AuthCode{get; set;}
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual System.DateTime? CreateDate{get; set;}

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? EndDate { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

