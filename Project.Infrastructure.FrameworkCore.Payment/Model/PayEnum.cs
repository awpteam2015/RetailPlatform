using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Payment.Model
{
    /// <summary>
    /// 支付类型枚举
    /// </summary>
    public enum PayEnum
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay = 0,
        /// <summary>
        /// 招商银行
        /// </summary>
        [Description("招商银行")]
        CmbBank = 1,
        /// <summary>
        /// 交通银行
        /// </summary>
        [Description("交通银行")]
        CommBank = 2,
        /// <summary>
        /// 工商银行
        /// </summary>
        [Description("工商银行")]
        IcbcBank = 3,
        /// <summary>
        /// 支付宝手机
        /// </summary>
        [Description("支付宝手机")]
        AlipayForMobile = 4,
        /// <summary>
        /// 财付通
        /// </summary>
        [Description("财付通")]
        Tenpay = 5
    }
}
