using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Payment.Model
{
    /// <summary>
    /// 招商银行支付实体
    /// </summary>
    internal class CmbBankModel
    {
        /// <summary>
        /// 网关地址
        /// </summary>
        public string CmbGateway { get; set; }
        /// <summary>
        /// 支付结果通知地址
        /// </summary>
        public string CmbReturnUrl { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string CmbKey { get; set; }
        /// <summary>
        /// 商户开户分行号
        /// </summary>
        public string CmbBranchId { get; set; }
        /// <summary>
        /// 商户号，6位数字
        /// </summary>
        public string CmbCoNo { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 支付备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
