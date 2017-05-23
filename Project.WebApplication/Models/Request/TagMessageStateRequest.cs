using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class TagMessageStateRequest
    {
        /// <summary>
        /// 1 代表公告的 已读记录  2代表曝光的已读记录
        /// </summary>
        public virtual System.Int32? Kind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserName { get; set; }

        /// <summary>
        /// 公告id  或者问题id
        /// </summary>
        public virtual System.Int32? MessageId { get; set; }
    }
}