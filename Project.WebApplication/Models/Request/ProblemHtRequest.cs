using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ProblemHtRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 问题申请单号
        /// </summary>
        public int RiverProblemApplyId { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }



    }
}