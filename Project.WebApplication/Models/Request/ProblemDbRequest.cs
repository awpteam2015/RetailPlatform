using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ProblemDbRequest
    {
        /// <summary>
        /// 督办人编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 督办人名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 督办抄送人
        /// </summary>
        public string DbCsUserCode { get; set; }

        /// <summary>
        /// 督办人
        /// </summary>
        public string DbUserCode { get; set; }

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