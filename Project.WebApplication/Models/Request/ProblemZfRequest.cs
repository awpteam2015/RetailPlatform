using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ProblemZfRequest
    {
        /// <summary>
        /// 问题申请单号
        /// </summary>
        public int RiverProblemApplyId { get; set; }

        /// <summary>
        /// 转发备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 转发人
        /// </summary>
        public string ToUserCode { get; set; }

        /// <summary>
        /// 转发抄送人
        /// </summary>

        public string ZfCsUserCode { get; set; }


    }
}