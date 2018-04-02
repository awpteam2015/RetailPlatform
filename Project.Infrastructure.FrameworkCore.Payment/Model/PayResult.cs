using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Payment.Model
{
    /// <summary>
    /// 支付结果实体
    /// </summary>
    public class PayResult
    {
        public PayResult()
        {
            Status = false;
        }

        /// <summary>
        /// 商家账号
        /// </summary>
        public string MerchantAccount { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public string TotalAmount { get; set; }

        /// <summary>
        /// 支付日期
        /// </summary>
        public string PayDate { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public PayEnum PayEnum { get; set; }

        /// <summary>
        /// 验证信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 【重写ToString】返回支付文本信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("支付银行:{0}|", GetPayEnumDescription(PayEnum));
            sb.AppendFormat("商户号:{0}|", MerchantAccount);
            sb.AppendFormat("订单号:{0}|", OrderNo);
            sb.AppendFormat("支付总金额:{0}|", TotalAmount);
            sb.AppendFormat("交易流水号:{0}|", SerialNumber);
            sb.AppendFormat("交易的日期:{0}", PayDate);
            return sb.ToString();
        }

        /// <summary>
        /// 获取支付名称
        /// </summary>
        /// <param name="payEnum"></param>
        /// <returns></returns>
        public static string GetPayEnumDescription(PayEnum payEnum)
        {
            var type = typeof(PayEnum);
            var name = Enum.GetName(typeof(PayEnum), payEnum);
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var objs = type.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (objs.Length == 0)
                return string.Empty;

            var attr = objs[0] as DescriptionAttribute;
            return attr.Description;
        }
    }
}
