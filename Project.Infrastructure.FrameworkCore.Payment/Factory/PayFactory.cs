using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Payment.Interfaces;
using Project.Infrastructure.FrameworkCore.Payment.Model;

namespace Project.Infrastructure.FrameworkCore.Payment.Factory
{
    /// <summary>
    /// 支付服务
    /// </summary>
    public class PayFactory : IPayFactory
    {
        /// <summary>
        /// 发送支付请求
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public string SubmitRequest(OrderPay paymentModel)
        {
            var result = DataAccess.GetCreate(paymentModel.PayCode).SubmitRequest(paymentModel);
            return result;
        }

        /// <summary>
        /// 处理支付返回信息
        /// </summary>
        /// <param name="payEnum"></param>
        /// <param name="notifyEnum"></param>
        /// <returns></returns>
        public PayResult CheckNotifyData(PayEnum payEnum, NotifyEnum notifyEnum)
        {
            var result = DataAccess.GetCreate(payEnum).CheckNotifyData(notifyEnum);
            return result;
        }
    }
}
