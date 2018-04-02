using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Payment.Model
{
    /// <summary>
    /// 支付实体信息
    /// 支付宝需要传入收货人信息
    /// </summary>
    public class OrderPay
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 支付代码
        /// </summary>
        public string PayCode { get; set; }

        ///// <summary>
        ///// 当前支付时间（可空，调用时统一处理）
        ///// </summary>
        //public string PayDate { get; set; }

        #region 收货人信息     
        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiveName { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        public string ReceiveAddress { get; set; }

        /// <summary>
        /// 收货人邮编
        /// </summary>
        public string ReceiveZip { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceivePhone { get; set; }

        /// <summary>
        /// 收货人手机
        /// </summary>
        public string ReceiveMobile { get; set; }
        #endregion
    }
}
