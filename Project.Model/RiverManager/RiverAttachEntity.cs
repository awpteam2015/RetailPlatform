

/***************************************************************************
*       功能：     RMRiverAttach实体类
*       作者：     李伟伟
*       日期：     2016/7/30
*       描述：     河流水质水纹管理
* *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class RiverAttachEntity : Entity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? RiverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String RiverName { get; set; }
        /// <summary>
        /// 记录月份
        /// </summary>
        public virtual System.DateTime? RecordTime { get; set; }
        /// <summary>
        /// 水纹水质
        /// </summary>
        public virtual System.String Remark { get; set; }


        /// <summary>
        /// 2代表下降
        /// </summary>
        public virtual System.Int32 WaterQualityChange { get; set; }
        public virtual System.String RiverChief { get; set; }
        public virtual System.String RiverArea { get; set; }
        public virtual System.String PointName { get; set; }
        public virtual System.String RiverFlow { get; set; }
        public virtual System.String Zb1 { get; set; }
        public virtual System.String Zb2 { get; set; }
        public virtual System.String Zb3 { get; set; }
        public virtual System.String WaterQualityRank { get; set; }
        public virtual System.String Pointer { get; set; }

        public virtual System.Int32 Day { get; set; }

        public virtual System.Int32 Month { get; set; }
        public virtual System.Int32 Year { get; set; }
        public virtual System.Int32 IsMainData { get; set; }



        #endregion


        #region 新增属性
        public virtual string Att_RecordTime
        {
            get { return RecordTime.GetValueOrDefault().ToString("yyyy-MM"); }
        }


        #endregion
    }
}




