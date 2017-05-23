using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    /// <summary>
    /// 问题状态 1.部门待处理 2河长待处理 3完结 4重新申请作废 5回退部门待处理 
    /// </summary>
    public class GetProblemListRequest
    {

        /// <summary>
        /// 问题标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 登录人编号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 是否处理 0代表全部 1代表未处理 2代表已处理
        /// </summary>
        public int IsDeal { get; set; }

        /// <summary>
        /// 是否曝光
        /// </summary>

        public int IsExposure { get; set; }

        /// <summary>
        /// 河长姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 上报人
        /// </summary>
        public string ApplyManName { get; set; }

        /// <summary>
        /// 河道级别
        /// </summary>
        public string RiverRank { get; set; }

        /// <summary>
        /// 问题开始时间
        /// </summary>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// 问题结束时间
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Des { get; set; }

        /// <summary>
        /// 分页开始记录位置
        /// </summary>
        public int skipResults { get; set; }

        /// <summary>
        /// 分页结束记录位置
        /// </summary>
        public int maxResults { get; set; }
    }

}