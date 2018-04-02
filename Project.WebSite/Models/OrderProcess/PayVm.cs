using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebSite.Models.OrderProcess
{
    public class PayVm
    {
        /// <summary>
        /// 
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public decimal Totalamount { get; set; }
    }
}