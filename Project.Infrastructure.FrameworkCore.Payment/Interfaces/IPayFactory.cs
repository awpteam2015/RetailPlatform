using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Payment.Model;

namespace Project.Infrastructure.FrameworkCore.Payment.Interfaces
{
    /// <summary>
    /// 支付工厂接口
    /// </summary>
    public interface IPayFactory
    {
        /// <summary>
        /// 发送支付请求
        /// </summary>
        /// <param name="paymentModel">支付信息</param>
        /// <returns></returns>
        string SubmitRequest(OrderPay paymentModel);

        /// <summary>
        /// 处理支付返回信息
        /// </summary>
        /// <param name="payType">支付类型</param>
        /// <param name="notifyType">参数</param>
        /// <returns></returns>
        PayResult CheckNotifyData(PayEnum payType, NotifyEnum notifyType);
    }
}
