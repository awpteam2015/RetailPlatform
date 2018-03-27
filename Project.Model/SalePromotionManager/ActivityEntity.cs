

/***************************************************************************
*       功能：     SPMActivity实体类
*       作者：     李伟伟
*       日期：     2018/3/26
*       描述：     促销活动  目前考虑单品促销 满足金额发券 满足金额减免
* *************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.SalePromotionManager
{
    public class ActivityEntity : Entity
    {
        public ActivityEntity()
        {
            RuleEntityList = new HashSet<RuleEntity>();
        }

        #region 属性
        /// <summary>
        /// 促销主题
        /// </summary>
        public virtual System.String Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 State { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public virtual System.String BriefDescription { get; set; }
        #endregion


        #region 新增属性
        public virtual System.String Att_State
        {
            get
            {
                if (State == 1)
                {
                    return "启用";
                }
                else
                {
                    return "关闭";
                }
            }
        }

        public virtual string Kind
        {
            get { return "Activity"; }
        }

        public virtual int IsHasRule
        {
            get { return RuleEntityList.Any() ? 1 : 0; }
        }


        public virtual ISet<RuleEntity> RuleEntityList { get; set; }

        #endregion
    }
}




