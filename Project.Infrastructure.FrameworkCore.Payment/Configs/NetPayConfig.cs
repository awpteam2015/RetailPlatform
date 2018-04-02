using System;
using System.Configuration;
using System.Linq;
using Project.Infrastructure.FrameworkCore.Payment.Model;

namespace Project.Infrastructure.FrameworkCore.Payment.Configs
{
    /// <summary>
    /// 支付配置信息
    /// </summary>
    public class NetPayConfig
    {
        /// <summary>
        /// 网店刷卡-招行网银
        /// </summary>
        public static string CmbPayCode = ConfigurationManager.AppSettings["CmbPayCode"];

        /// <summary>
        /// 网店刷卡-工行网银
        /// </summary>
        public static string IcbcPayCode = ConfigurationManager.AppSettings["IcbcPayCode"];

        /// <summary>
        /// 网店刷卡-交行网银
        /// </summary>
        public static string CommPayCode = ConfigurationManager.AppSettings["CommPayCode"];

        /// <summary>
        /// 支付宝
        /// </summary>
        public static string AlipayCode = ConfigurationManager.AppSettings["AlipayCode"];

        /// <summary>
        /// 支付宝
        /// </summary>
        public static string TenpayCode = ConfigurationManager.AppSettings["TenpayCode"];

        /// <summary>
        /// 招商汇转
        /// </summary>
        public static string CmbRemittancePayCode = ConfigurationManager.AppSettings["CmbRemittancePayCode"];

        /// <summary>
        /// 工行汇转
        /// </summary>
        public static string IcbcRemittancePayCode = ConfigurationManager.AppSettings["IcbcRemittancePayCode"];

        /// <summary>
        /// 交行汇转
        /// </summary>
        public static string CommRemittancePayCode = ConfigurationManager.AppSettings["CommRemittancePayCode"];

        /// <summary>
        /// 判断是否汇款
        /// </summary>
        /// <param name="payCode">支付代码</param>
        /// <returns></returns>
        public static bool IsRemittance(string payCode)
        {
            return (CmbRemittancePayCode ?? string.Empty).Split(',').Contains(payCode) ||
                   (IcbcRemittancePayCode ?? string.Empty).Split(',').Contains(payCode) ||
                   (CommRemittancePayCode ?? string.Empty).Split(',').Contains(payCode);
        }

        /// <summary>
        /// 通过支付枚举获取支付代码
        /// </summary>
        /// <param name="payEnum"></param>
        /// <returns></returns>
        public static string GetPayCode(PayEnum payEnum)
        {
            string result;
            switch (payEnum)
            {
                case PayEnum.Alipay:
                    result = AlipayCode;
                    break;
                case PayEnum.CmbBank:
                    result = CmbPayCode;
                    break;
                case PayEnum.CommBank:
                    result = CommPayCode;
                    break;
                case PayEnum.IcbcBank:
                    result = IcbcPayCode;
                    break;
                case PayEnum.Tenpay:
                    result = TenpayCode;
                    break;
                default:
                    throw new InvalidOperationException("无效的支付类型，支付异常。"); 
            }
            return result;
        }
    }
}
