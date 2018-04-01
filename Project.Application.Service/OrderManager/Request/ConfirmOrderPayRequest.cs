using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Application.Service.Common;

namespace Project.Application.Service.OrderManager.Request
{
   public class ConfirmOrderPayRequest:RequestBase
    {
       public string OrderNo { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual System.Int32? PayType { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public virtual System.String PaySerialNumber { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public virtual System.DateTime? PayTime { get; set; }
    }
}
