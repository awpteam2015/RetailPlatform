using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Payment.Model
{
    /// <summary>
    /// 支付宝支付实体
    /// </summary>
    internal class AlipayModel
    {
        /// <summary>
        /// 合作身份者ID
        /// </summary>
        public string Partner { get; set; }
        /// <summary>
        /// 交易安全检验码
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 服务器异步通知页面路径
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 页面跳转同步通知页面路径
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 卖家支付宝帐户
        /// </summary>
        public string SellerEmail { get; set; }
        /// <summary>
        /// 支付备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
