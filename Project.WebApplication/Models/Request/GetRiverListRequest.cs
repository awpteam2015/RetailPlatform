using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class GetRiverListRequest
    {
        /// <summary>
        /// 河道名称
        /// </summary>
        public string RiverName { get; set; }

        /// <summary>
        /// 归属部门
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 河道等级
        /// </summary>
        public string RiverRank { get; set; }

        /// <summary>
        /// 河道起点
        /// </summary>
        public virtual System.String RiverStart { get; set; }

        /// <summary>
        /// 河道终点
        /// </summary>
        public virtual System.String RiverEnd { get; set; }

        /// <summary>
        /// 流经乡（镇）
        /// </summary>
        public virtual System.String RiverCrossArea { get; set; }
        /// <summary>
        /// 河长姓名
        /// </summary>
        public virtual System.String UserName { get; set; }

        /// <summary>
        /// 河长手机
        /// </summary>
        public virtual System.String UserCode { get; set; }


        public int skipResults { get; set; }


        public int maxResults { get; set; }
    }
}