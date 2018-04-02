using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Payment.Model
{
    /// <summary>
    /// 财付通
    /// </summary>
    public class TenpayModel
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public string Partner { get; set; }

        /// <summary>
        /// 财付通密钥
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
        /// 支付备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
