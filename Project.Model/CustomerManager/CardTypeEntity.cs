

 /***************************************************************************
 *       功能：     CMCardType实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     会员卡类型
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.CustomerManager
{
    public class CardTypeEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        public virtual System.String CardtypeName{get; set;}
        /// <summary>
        /// 折扣
        /// </summary>
        public virtual System.Int32 Discount{get; set;}

        public virtual decimal NeedTotalAmount { get; set; }
        #endregion


        #region 新增属性

        #endregion
    }
}

    
 

