using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Payment.Configs;
using Project.Infrastructure.FrameworkCore.Payment.Interfaces;
using Project.Infrastructure.FrameworkCore.Payment.Model;
using Project.Infrastructure.FrameworkCore.Payment.Services;

namespace Project.Infrastructure.FrameworkCore.Payment.Factory
{
    /// <summary>
    /// 获得支付访问层
    /// </summary>
    public class DataAccess
    {
        private static IPayService _payCreate;

        /// <summary>
        /// 创建支付创建者
        /// </summary>
        /// <param name="payEnum">支付类型</param>
        /// <returns></returns>
        public static IPayService GetCreate(PayEnum payEnum)
        {
            switch (payEnum)
            {
                case PayEnum.Alipay:
                    _payCreate = new AlipayService();
                    break;
                //case PayEnum.CmbBank:
                //    _payCreate = new CmbBankService();
                    break;
                case PayEnum.CommBank:
                    _payCreate = new CommBankService();
                    break;
                //case PayEnum.IcbcBank:
                //    _payCreate = new IcbcBankService();
                    break;
                case PayEnum.Tenpay:
                    _payCreate = new TenpayService();
                    break;
                default:
                    throw new InvalidOperationException("无效的支付类型，支付异常。");
            }
            return _payCreate;
        }

        /// <summary>
        /// 创建支付创建者
        /// </summary>
        /// <param name="payCode">支付类型</param>
        /// <returns></returns>
        public static IPayService GetCreate(string payCode)
        {
            if (payCode == NetPayConfig.AlipayCode)
            {
                _payCreate = new AlipayService();
            }
            //else if (payCode == NetPayConfig.CmbPayCode)
            //{
            //    _payCreate = new CmbBankService();
            //}
            else if (payCode == NetPayConfig.CommPayCode)
            {
                _payCreate = new CommBankService();
            }
            //else if (payCode == NetPayConfig.IcbcPayCode)
            //{
            //    _payCreate = new IcbcBankService();
            //}
            else if (payCode == NetPayConfig.TenpayCode)
            {
                _payCreate = new TenpayService();
            }
            else
            {
                throw new InvalidOperationException("无效的支付类型，支付异常。");
            }
            return _payCreate;
        }
    }
}
