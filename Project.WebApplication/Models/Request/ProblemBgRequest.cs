using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ProblemBgRequest
    {
        /// <summary>
        /// 问题申请单号
        /// </summary>
        public int RiverProblemApplyId { get; set; }

        /// <summary>
        /// 是否曝光
        /// </summary>
        public int IsExposure { get; set; }

        /// <summary>
        /// 曝光等级
        /// </summary>
        public int ExposureLever { get; set; }
    }
}