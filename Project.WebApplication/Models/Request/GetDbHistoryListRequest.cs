using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class GetDbHistoryListRequest
    {
        /// <summary>
        /// 联系电话
        /// </summary>
        public int RiverProblemApplyId { get; set; }


        public int skipResults { get; set; }


        public int maxResults { get; set; }
    }
}